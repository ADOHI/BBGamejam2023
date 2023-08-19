using BBGamejam.Envirionment.Underwater.Terrain;
using BBGamejam.Global.Ingame;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Envirionment.Underwater.Kelp
{
    public class KelpGenerator : MonoBehaviour
    {
        private float cameraDistance = 10f;
        public List<GameObject> kelpPrefabs;
        public float minSpawnXPosInterval;
        public float maxSpawnXPosInterval;
        public float xOffset;
        public float yOffset;
        public float minDepth;
        public float maxDepth;
        public float minScale;
        public float maxScale;
        private void Start()
        {
            SpawnAsync(0f).AttachExternalCancellation(destroyCancellationToken).Forget();
        }

        private void Spawn(Vector3 initialPosition)
        {
            var index = Random.Range(0, kelpPrefabs.Count);

            var spawnedKelp = Instantiate(kelpPrefabs[index]);

            var depth = Random.Range(minDepth, maxDepth);

            var interporatedYPos = yOffset * (1f + depth / cameraDistance);

            spawnedKelp.transform.SetParent(transform);
            spawnedKelp.transform.position = initialPosition + new Vector3(xOffset, interporatedYPos, depth);
            spawnedKelp.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
            spawnedKelp.SetActive(true);
        }

        private async UniTask SpawnAsync(float previousXPos)
        {
            var nextPos = previousXPos + Random.Range(minSpawnXPosInterval, maxSpawnXPosInterval);

            await UniTask.WaitUntil(() => IngameManager.Instance.turtle.Value.transform.position.x > nextPos);

            Spawn(IngameManager.Instance.turtle.Value.transform.position);

            SpawnAsync(nextPos).Forget();
        }

    }

}
