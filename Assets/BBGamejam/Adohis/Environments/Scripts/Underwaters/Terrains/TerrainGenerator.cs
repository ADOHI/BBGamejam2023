using BBGamejam.Global.Ingame;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Envirionment.Underwater.Terrain
{
    public class TerrainGenerator : MonoBehaviour
    {
        public enum TerrainDistanceType
        {
            Near,
            Middle,
            Background
        }

        private float cameraDistanceToCharacter = 10f;
        private GameObject[,] terrainBlocks;
        public TerrainChunk chunkPrefab;
        public List<GameObject> rockPrefabs;

        [Header("SpawnConfig")]
        public TerrainSpawnConfig nearConfig;
        public TerrainSpawnConfig middleConfig;
        public TerrainSpawnConfig farConfig;

        private void Awake()
        {
            InitialSpawn();
        }

        private void Start()
        {
            SpawnAsync(nearConfig, 10f).Forget();
        }

        private void InitialSpawn()
        {
            SpawnNearBackground(new Vector3(-90f + Random.Range(-5f, 5f), 0f, 0f));
            SpawnNearBackground(new Vector3(-60f + Random.Range(-5f, 5f), 0f, 0f));
            SpawnNearBackground(new Vector3(-30f + Random.Range(-5f, 5f), 0f, 0f));
            SpawnNearBackground(new Vector3(0f + Random.Range(-5f, 5f), 0f, 0f));
        }

        private async UniTask SpawnAsync(TerrainSpawnConfig config, float previousXPos)
        {
            var nextPos = previousXPos + Random.Range(config.minSpawnXPosInterval, config.maxSpawnXPosInterval);

            await UniTask.WaitUntil(() => IngameManager.Instance.turtle.Value.transform.position.x > nextPos);

            SpawnNearBackground(IngameManager.Instance.turtle.Value.transform.position);

            SpawnAsync(config, nextPos).Forget();
        }

        private void SpawnNearBackground(Vector3 initialPosition)
        {
            Spawn(nearConfig, initialPosition);
        }

        private void SpawnMiddleBackground(Vector3 initialPosition)
        {
            Spawn(middleConfig, initialPosition);

        }

        private void SpawnFarBackground(Vector3 initialPosition)
        {
            Spawn(farConfig, initialPosition);

        }

        [Button]
        private void Spawn(TerrainDistanceType type)
        {
            var initialPosition = IngameManager.Instance.turtle.Value.transform.position;
            switch (type)
            {
                case TerrainDistanceType.Near:
                    SpawnNearBackground(initialPosition);
                    break;
                case TerrainDistanceType.Middle:
                    SpawnMiddleBackground(initialPosition);
                    break;
                case TerrainDistanceType.Background:
                    SpawnFarBackground(initialPosition);
                    break;
            }
        }

        [Button]
        private void Spawn(TerrainSpawnConfig config, Vector3 initialPosition)
        {
            var gridWidth = Random.Range(config.minGridWidth, config.maxGridWidth + 1);
            var gridHeight = Random.Range(config.minGridHeight, config.maxGridHeight + 1);
            var xOffset = Random.Range(config.minXOffset, config.maxXOffset);
            var yOffset = Random.Range(config.minYOffset, config.maxYOffset);
            var xzScale = Random.Range(config.minXZScale, config.maxXZScale);
            var yScale = Random.Range(config.minYScale, config.maxYScale);

            var terrain = Spawn(config, gridWidth, gridHeight, xOffset, yOffset, xzScale, yScale);

            var zPosition = Random.Range(config.minZPosition, config.maxZPosition);

            var spawnOffset = new Vector3(config.spawnXPositionOffset, config.spawnYPositionOffset * (1f + zPosition / cameraDistanceToCharacter), zPosition);
            
            terrain.transform.position = initialPosition + spawnOffset;
            /*terrainBlocks = new GameObject[gridWidth, gridHeight];

            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    var spawnedObject = Instantiate(rockPrefabs[Random.Range(0, rockPrefabs.Count)]);
                    terrainBlocks[i, j] = spawnedObject;
                    //spawnedObject.transform.position = new Vector3(i, 0f, j);
                    //spawnedObject.transform.localScale = new Vector3(1f, Mathf.PerlinNoise(i * xOffset, j * yOffset), 1f);
                    spawnedObject.transform.SetParent(transform);
                    spawnedObject.SetActive(true);
                }
            }

            SetScaleAndPosition();*/
        }

        [Button]
        private TerrainChunk Spawn(TerrainSpawnConfig config, int gridWidth, int gridHeight, float xOffset, float yOffset, float xzScale, float yScale)
        {
            var terrainChunk = Instantiate(chunkPrefab);
            terrainChunk.transform.SetParent(transform);

            var spawnedBlocks = new GameObject[gridWidth, gridHeight];

            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    var spawnedObject = Instantiate(rockPrefabs[Random.Range(0, rockPrefabs.Count)]);
                    spawnedBlocks[i, j] = spawnedObject;
                    spawnedObject.transform.SetParent(terrainChunk.transform);
                    spawnedObject.SetActive(true);
                }
            }

            terrainChunk.transform.localScale = new Vector3(xzScale, yScale, xzScale);

            SetScaleAndPosition(config, xOffset, yOffset, spawnedBlocks);
            terrainChunk.gameObject.SetActive(true);
            return terrainChunk;
        }

/*        [Button]
        private void SetScaleAndPosition()
        {
            if (terrainBlocks == null)
            {
                return;
            }

            var maxDistance =  Mathf.Sqrt(Mathf.Pow(terrainBlocks.GetLength(0) * 0.5f, 2f) + Mathf.Pow(terrainBlocks.GetLength(1) * 0.5f, 2f));
            for (int i = 0; i < terrainBlocks.GetLength(0); i++)
            {
                for (int j = 0; j < terrainBlocks.GetLength(1); j++)
                {
                    var centerX = terrainBlocks.GetLength(0) * 0.5f;
                    var centerZ = terrainBlocks.GetLength(1) * 0.5f;

                    var distanceToCenter = Mathf.Sqrt(Mathf.Pow(centerX - i, 2f) + Mathf.Pow(centerZ - j, 2f));
                    var distanceToFar = j;

                    var distanceToCenterRatio = 1f - (distanceToCenter / maxDistance);
                    var distanceToFarRatio = (float)j / (terrainBlocks.GetLength(1) - 1);

                    var yScale = 
                        Mathf.PerlinNoise(i * xOffset, j * yOffset) * 
                        distanceToFarRatio * depthCorrection *
                        distanceToCenterRatio * centerCorrection;

                    var terrainScale = (yScale > minYScaleThresholed) ?
                        new Vector3(Random.Range(1f, 2f), yScale, Random.Range(1f, 2f)) :
                        Vector3.zero;
                    
                    terrainBlocks[i, j].transform.localScale = terrainScale;
                    terrainBlocks[i, j].transform.localPosition = new Vector3(i, yScale * heightCorrection, j);
                }
            }
        }*/

        [Button]
        private void SetScaleAndPosition(TerrainSpawnConfig config, float xOffset, float yOffset, GameObject[,] terrainBlocks)
        {
            if (terrainBlocks == null)
            {
                return;
            }

            var maxDistance =  Mathf.Sqrt(Mathf.Pow(terrainBlocks.GetLength(0) * 0.5f, 2f) + Mathf.Pow(terrainBlocks.GetLength(1) * 0.5f, 2f));
            for (int i = 0; i < terrainBlocks.GetLength(0); i++)
            {
                for (int j = 0; j < terrainBlocks.GetLength(1); j++)
                {
                    var centerX = terrainBlocks.GetLength(0) * 0.5f;
                    var centerZ = terrainBlocks.GetLength(1) * 0.5f;

                    var distanceToCenter = Mathf.Sqrt(Mathf.Pow(centerX - i, 2f) + Mathf.Pow(centerZ - j, 2f));
                    var distanceToFar = j;

                    var distanceToCenterRatio = 1f - (distanceToCenter / maxDistance);
                    var distanceToFarRatio = (float)distanceToFar / (terrainBlocks.GetLength(1) - 1);

                    var yScale =
                        Mathf.PerlinNoise(i * xOffset, j * yOffset) *
                        distanceToFarRatio * config.depthCorrection *
                        distanceToCenterRatio * config.centerCorrection;

                    var terrainScale = (yScale > config.minYScaleThresholed) ?
                        new Vector3(Random.Range(config.minXZMultiply, config.maxXZMultiply), yScale, Random.Range(config.minXZMultiply, config.maxXZMultiply)) :
                        Vector3.zero;

                    terrainBlocks[i, j].transform.localScale = terrainScale;
                    terrainBlocks[i, j].transform.localPosition = new Vector3(i, yScale * config.heightCorrection, j);
                }
            }
        }
    }

}
