using AtmosphericHeightFog;
using BBGamejam.Global.Ingame;
using BBGamejam.Global.Particles;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using Pixelplacement;
using RabbitResurrection;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace BBGamejam.Global.Mode
{
    public class SlowModeManager : Singleton<SlowModeManager>
    {
        private float slowModeProgress;
        private float easedSlowModeProgress;
        [ShowInInspector] private SkinnedMeshRenderer playerCharacterRenderer;
        private float slowModeDuration;
        private SoundManager.SoundManagerAudio chargingAudio;
        public bool isSlowMode;
        public float fadeInOutTime = 0.5f;
        public float slowModeTimeScale = 0.1f;
        public Ease fadeInEase;
        public Ease fadeOutEase;
        public HeightFogGlobal underwaterFog;

        [Header("CharacterEmission")]
        public float twinkleInterval;
        public Color emissionColor;
        public float emissionHDR;

        [Header("ZoomIn")]
        public float zoomInMultiply = 0.75f;

        [Header("Sfx")]
        public AudioClip chargingSfx;
        public AudioClip jumpSfx;
        public AudioClip landingSfx;

        private void Start()
        {         
            //playerCharacterRenderer = IngameManager.Instance.rabbit.Value.GetComponentInChildren<SkinnedMeshRenderer>();
            playerCharacterRenderer = IngameManager.Instance.rabbit.Value.GetComponentInParent<RabbitResurrection.Rabbit>().targetRenderer;
        }

        private void Update()
        {
            if (IngameManager.Instance.isGamePause)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                isSlowMode = true;
                chargingAudio = SoundManager.PlayFx(chargingSfx, 1, true);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                BubbleGenerator.Instance.Explosion(IngameManager.Instance.turtle.Value.transform.position);
                isSlowMode = false;
                SoundManager.StopFx(chargingAudio);

                SoundManager.PlayFx(jumpSfx, 1);

            }

            UpdateProgress();
            if (!Managers.Game.IsGameOver)
            {
                UpdateSlowMode(this.easedSlowModeProgress);
            }
        }

        private void UpdateProgress()
        {
            if (isSlowMode)
            {
                slowModeDuration += Time.unscaledDeltaTime;
                slowModeProgress = Mathf.Clamp01(slowModeProgress + Time.unscaledDeltaTime / fadeInOutTime);
                easedSlowModeProgress = DOVirtual.EasedValue(0f, 1f, slowModeProgress, fadeInEase);
            }
            else
            {
                slowModeDuration = 0f;
                slowModeProgress = Mathf.Clamp01(slowModeProgress - Time.unscaledDeltaTime / fadeInOutTime);
                easedSlowModeProgress = DOVirtual.EasedValue(0f, 1f, slowModeProgress, fadeOutEase);

            }
        }

        private void UpdateSlowMode(float slowModeProgress)        
        {
            
            
            underwaterFog.skyboxFogIntensity = 1f - slowModeProgress;
            
            if (isSlowMode)
            {
                Time.timeScale = Mathf.Clamp((1f - slowModeProgress), slowModeTimeScale, 1f);
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            else
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            TwinkleCharacter(1f - Mathf.Abs((slowModeDuration % twinkleInterval) / twinkleInterval * 2f - 1f));
        }

        private void TwinkleCharacter(float twinkleProgress)
        {
            playerCharacterRenderer
                .material
                .SetColor("_EmissionColor", Color.Lerp(Color.black, emissionColor * emissionHDR, twinkleProgress));
        }

        private void ZoomIn(float zoomInMultiply)
        {
            var ingameScene =  (Managers.Scene.CurrentScene as InGameScene);
            ingameScene.CineTarget.SetBodyDistance(ingameScene.cameraDistance * zoomInMultiply);
        }

        public void PlayLandingSound()
        {
            SoundManager.PlayFx(landingSfx, 1);
        }

        public void UpgradeSlowMode()
        {
            slowModeTimeScale *= 0.8f;
        }
    }

}
