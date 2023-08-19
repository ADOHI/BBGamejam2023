using Cysharp.Threading.Tasks;
using DistantLands;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace BBGamejam.Envirionment.Underwater
{
    public class UnderwaterGenerator : MonoBehaviour
    {
        public GameObjectReference turtleGameobject;
        public GameObject underWaterForSpawn;
        public GameObject fishesForSpawn;

        [Header("Fishes")]
        public GlobalFlock fishFlockPrefab;
        public float minSpawnHeight;
        public float maxSpawnHeight;
        public float minSpawnDepth;
        public float maxSpawnDepth;
        public float minSpawnInterval;
        public float maxSpawnInterval;
        public float minWanderSize;
        public float maxWanderSize;
        public int minNumFishSize;
        public int maxNumFishSize;
        public float spawnXOffset;
        private void Start()
        {
            SpawnAsync(turtleGameobject.Value.transform.position.x).Forget();
        }

        public async UniTask SpawnAsync(float currentSpawnPos)
        {
            await UniTask.WaitUntil(() => turtleGameobject.Value.transform.position.x > currentSpawnPos);
            Spawn(turtleGameobject.Value.transform.position, spawnXOffset);
            var nextSpawnXPos = currentSpawnPos + Random.Range(minSpawnInterval, maxSpawnInterval);
            SpawnAsync(nextSpawnXPos).Forget();
        }

        public void Spawn(Vector3 turtlePosition, float xOffset)
        {

            var spawnedFish = Instantiate(fishFlockPrefab);
            var spawnedPosition =
                turtlePosition +
                Vector3.up * Random.Range(minSpawnHeight, maxSpawnHeight) +
                Vector3.forward * Random.Range(minSpawnDepth, maxSpawnDepth) +
                Vector3.right * xOffset;

            Debug.Log($"SpawnFishObject at {spawnedPosition}");


            spawnedFish.transform.SetParent(transform);
            spawnedFish.transform.position = spawnedPosition;

            spawnedFish.wanderSize = Random.Range(minWanderSize, maxWanderSize);
            spawnedFish.numFish = Random.Range(minNumFishSize, maxNumFishSize);

            spawnedFish.gameObject.SetActive(true);
        }

    }
}

