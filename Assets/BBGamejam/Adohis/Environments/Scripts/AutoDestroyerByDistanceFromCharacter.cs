using BBGamejam.Global.Ingame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Global.System
{
    public class AutoDestroyerByDistanceFromCharacter : MonoBehaviour
    {
        public float distance;

        // Update is called once per frame
        void Update()
        {
            if (transform.position.x < IngameManager.Instance.turtle.Value.transform.position.x - distance)
            {
                Destroy(gameObject);
            }
        }
    }
}

