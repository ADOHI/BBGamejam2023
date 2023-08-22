using BBGamejam.Global.Ingame;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Ingame.Skill
{
    public class SkillManager : Singleton<SkillManager>
    {
        private int nextUpgradeInterval;
        public GameObject passiveSkillCanvas;
        public int skillUpgradeInterval = 200;
        public AudioClip revealSfx;
        public AudioClip hoverSfx;
        public AudioClip selectionSfx;
        private void Awake()
        {
            nextUpgradeInterval = skillUpgradeInterval;
        }

        private void Update()
        {
            if (IngameManager.Instance.depthOfWater >= nextUpgradeInterval)
            {
                OnSkillUpgradeAvailable();

                nextUpgradeInterval += skillUpgradeInterval;
            }
        }

        public void OnSkillUpgradeAvailable()
        {
            IngameManager.Instance.PauseGame();
            passiveSkillCanvas.SetActive(true);
            SoundManager.PlayFx(revealSfx);
        }

        public void OnSkillUpgradeEnd()
        {
            IngameManager.Instance.ResumeGame();
            passiveSkillCanvas.SetActive(false);
        }
    }

}
