using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Global.Ingame
{
    public class IngameManager : Singleton<IngameManager>
    {
        public GameObject turtle;
        public GameObject rabbit;

        [Header("IngameProgress")]
        public float goalXPos;
        public float progress;
        [Header("Score")]
        public float score;


        private void Update()
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            progress = Mathf.Clamp01(turtle.transform.position.x / goalXPos);
        }
    }

}
