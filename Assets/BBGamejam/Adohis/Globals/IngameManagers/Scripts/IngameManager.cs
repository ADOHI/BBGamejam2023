using Pixelplacement;
using RabbitResurrection;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace BBGamejam.Global.Ingame
{
    public class IngameManager : Singleton<IngameManager>
    {
        public GameObjectReference turtle;
        public GameObjectReference rabbit;

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
            var zara = (Managers.Scene.CurrentScene as InGameScene).Zara;
            progress = zara.CalculateProgress();

            if(progress >= 1f)
            {
                Managers.Game.GameClear();
            }
        }
    }

}
