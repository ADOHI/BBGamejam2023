using BBGamejam.Global.Ingame;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BBGamejam.Envirionment.Underwater.Terrain
{
    public class TerrainChunk : MonoBehaviour
    {
        private GameObject turtle => IngameManager.Instance.turtle;
        public float destroyDistanceGap;


        private void Update()
        {
            if (turtle.transform.position.x > transform.position.x + destroyDistanceGap)
            {
                Destroy(gameObject);
            }
        }
    }

}
