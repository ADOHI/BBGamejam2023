using Pixelplacement;
using RabbitResurrection;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BBGamejam.Global.Ingame
{
    public class IngameManager : Singleton<IngameManager>
    {
        public GameObjectReference turtle;
        public GameObjectReference rabbit;

        [Header("IngameProgress")]
        public float goalXPos;
        public float progress;
        [Header("Combo")]
        public int currentComboCount;
        public int firstComboThreshold = 5;
        public int secondComboThreshold = 10;
        [Header("Score")]
        public IntReference score;
        [Header("Sounds")]
        public AudioClip ingameBgm;
        public AudioClip comboSfxFirst;
        public AudioClip comboSfxSecond;

        private void Awake()
        {
            score.Value = 0;
        }

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

        public void ToTitleScene()
        {
            SceneManager.LoadScene(1);
        }

        public void AddCombo()
        {
            currentComboCount++;
        }

        public void ResetCombo()
        {
            currentComboCount = 0;
        }

        public void PlayComboSound()
        {
            if (currentComboCount < firstComboThreshold)
            {

            }
            else if (currentComboCount < secondComboThreshold)
            {
                SoundManager.PlayFx(comboSfxFirst);
            }
            else
            {
                SoundManager.PlayFx(comboSfxSecond);
            }
        }

        public int BasicScore => currentComboCount * 10;
        public int BonusScore => currentComboCount * currentComboCount * 2;

        public void UpdateScore()
        {
            var calculatedScore = BasicScore + BonusScore;

            score.Value += calculatedScore;
        }

    }

}
