using BBGamejam.Global.Ingame;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Envirionment.Underwater.Sunshaft
{
    public class SunshaftGenerator : MonoBehaviour
    {
        private float cameraDistance = 10f;
        public ParticleSystem sunshaftPrefab;
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
            //var index = Random.Range(0, kelpPrefabs.Count);

            var spawnedSunshaft = Instantiate(sunshaftPrefab);

            var depth = Random.Range(minDepth, maxDepth);
            var scale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(minDepth, maxDepth, depth)) * Random.Range(0.9f, 1.1f);

            var interporatedYPos = yOffset * (1f + depth / cameraDistance);

            spawnedSunshaft.transform.SetParent(transform);
            spawnedSunshaft.transform.position = initialPosition + new Vector3(xOffset, interporatedYPos, depth);
            spawnedSunshaft.transform.localScale = Vector3.one * scale;
            spawnedSunshaft.gameObject.SetActive(true);
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
