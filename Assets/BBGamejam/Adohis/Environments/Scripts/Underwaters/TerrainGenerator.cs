using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Envirionment.Underwater.Terrain
{
    public class TerrainGenerator : MonoBehaviour
    {
        private GameObject[,] terrainBlocks;

        public GameObject prefab;
        public List<GameObject> prefabs;
        public float xOffset;
        public float yOffset;
        public int width;
        public int height;

        [Header("NoiseSetter")]
        public float distanceMultiply;


        void Update()
        {

        }

        [Button]
        private void Spawn()
        {
            terrainBlocks = new GameObject[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var spawnedObject = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
                    terrainBlocks[i, j] = spawnedObject;
                    spawnedObject.transform.position = new Vector3(i, 0f, j);
                    //spawnedObject.transform.localScale = new Vector3(1f, Mathf.PerlinNoise(i * xOffset, j * yOffset), 1f);
                    spawnedObject.transform.SetParent(transform);
                    spawnedObject.SetActive(true);
                }
            }
        }

        [Button]
        private void SetScale()
        {
            if (terrainBlocks == null)
            {
                return;
            }

            for (int i = 0; i < terrainBlocks.GetLength(0); i++)
            {
                for (int j = 0; j < terrainBlocks.GetLength(1); j++)
                {
                    var centerX = terrainBlocks.GetLength(0) * 0.5f;
                    var centerZ = terrainBlocks.GetLength(1) * 0.5f;

                    var distance = Mathf.Sqrt(Mathf.Pow(centerX - i, 2f) + Mathf.Pow(centerZ - j, 2f));
                    var terrainScale = new Vector3(Random.Range(1f, 2f), Mathf.PerlinNoise(i * xOffset, j * yOffset) * j / distance, Random.Range(1f, 2f));
                    
                    terrainBlocks[i, j].transform.localScale = terrainScale;
                }
            }
        }
    }

}
