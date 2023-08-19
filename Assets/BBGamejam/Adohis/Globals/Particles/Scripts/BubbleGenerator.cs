using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Global.Particles
{
    public class BubbleGenerator : Singleton<BubbleGenerator>
    {
        public GameObject explosionBubblePrefab;
        public float explosionDuration;



        public void Explosion(Vector3 position)
        {
            var explosion = Instantiate(explosionBubblePrefab);
            explosion.transform.position = position;
            explosion.SetActive(true);
            GameObject.Destroy(explosion, explosionDuration);
        }
    }
}

