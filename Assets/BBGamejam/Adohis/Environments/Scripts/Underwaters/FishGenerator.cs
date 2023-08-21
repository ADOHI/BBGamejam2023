using BBGamejam.Global.Ingame;
using Cysharp.Threading.Tasks;
using DistantLands;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace BBGamejam.Envirionment.Underwater
{
    public class FishGenerator : MonoBehaviour
    {
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

        private void Awake()
        {
            InitialSpawn();
        }

        private void Start()
        {
            SpawnAsync(IngameManager.Instance.turtle.Value.transform.position.x).AttachExternalCancellation(destroyCancellationToken).Forget();
        }

        private void InitialSpawn()
        {
            Spawn(Vector3.zero, -10f);
        }


        public async UniTask SpawnAsync(float currentSpawnPos)
        {
            await UniTask.WaitUntil(() => IngameManager.Instance.turtle.Value.transform.position.x > currentSpawnPos);
            Spawn(IngameManager.Instance.turtle.Value.transform.position, spawnXOffset);
            var nextSpawnXPos = currentSpawnPos + Random.Range(minSpawnInterval, maxSpawnInterval);
            SpawnAsync(nextSpawnXPos).AttachExternalCancellation(destroyCancellationToken).Forget();
        }

        public void Spawn(Vector3 turtlePosition, float xOffset)
        {

            var spawnedFish = Instantiate(fishFlockPrefab);
            var spawnedPosition =
                turtlePosition +
                Vector3.up * Random.Range(minSpawnHeight, maxSpawnHeight) +
                Vector3.forward * Random.Range(minSpawnDepth, maxSpawnDepth) +
                Vector3.right * xOffset;

            spawnedFish.transform.SetParent(transform);
            spawnedFish.transform.position = spawnedPosition;

            spawnedFish.wanderSize = Random.Range(minWanderSize, maxWanderSize);
            spawnedFish.numFish = Random.Range(minNumFishSize, maxNumFishSize);

            spawnedFish.gameObject.SetActive(true);
        }

    }
}

