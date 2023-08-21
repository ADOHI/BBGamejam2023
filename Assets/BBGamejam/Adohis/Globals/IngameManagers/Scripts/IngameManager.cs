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
        [Header("Sounds")]
        public AudioClip ingameBgm;

        private void Start()
        {
            SoundManager.StopAll();
            SoundManager.PlayMusic(ingameBgm, 1, true, 1f);
        }

        private void Update()
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            var zara = (Managers.Scene.CurrentScene as InGameScene).Zara;
            progress = zara.CalculateProgress();

            if(progress >= 0.999f)
            {
                SoundManager.StopAll();
                Managers.Game.GameClear();
            }
        }
    }

}
