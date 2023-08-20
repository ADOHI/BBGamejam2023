using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Global.Particles
{
    public class BubbleGenerator : Singleton<BubbleGenerator>
    {
        [Header("Bubble")]
        public GameObject explosionBubblePrefab;
        public float explosionDuration;

        [Header("Hit")]
        public GameObject hitParticlePrefab;
        public float hitDuration;
        public AudioClip hitSfx;

        public void Explosion(Vector3 position)
        {
            var explosion = Instantiate(explosionBubblePrefab);
            explosion.transform.position = position;
            explosion.SetActive(true);
            GameObject.Destroy(explosion, explosionDuration);
        }

        public void Hit(Vector3 position)
        {
            var hit = Instantiate(hitParticlePrefab);
            hit.transform.position = position;
            hit.SetActive(true);
            GameObject.Destroy(hit, hitDuration);
            SoundManager.PlayFx(hitSfx);
        }
    }
}

