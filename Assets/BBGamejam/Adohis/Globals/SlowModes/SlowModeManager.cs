using AtmosphericHeightFog;
using BBGamejam.Global.Particles;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace BBGamejam.Global.Mode
{
    public class SlowModeManager : MonoBehaviour
    {
        private float slowModeProgress;
        private float easedSlowModeProgress;
        private MeshRenderer playerCharacterRenderer;
        private float slowModeDuration;
        public GameObjectReference playerCharacterObject;
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

        private void Start()
        {
            playerCharacterRenderer = playerCharacterObject.Value.GetComponentInChildren<MeshRenderer>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSlowMode = true;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                BubbleGenerator.Instance.Explosion(playerCharacterObject.Value.transform.position);
                isSlowMode = false;
            }

            UpdateProgress();
            UpdateSlowMode(this.easedSlowModeProgress);
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
            }
            else
            {
                Time.timeScale = 1f;
            }
            TwinkleCharacter(1f - Mathf.Abs((slowModeDuration % twinkleInterval) / twinkleInterval * 2f - 1f));
        }

        private void TwinkleCharacter(float twinkleProgress)
        {
            playerCharacterRenderer
                .material
                .SetColor("_EmissionColor", Color.Lerp(Color.black, emissionColor * emissionHDR, twinkleProgress));
        }
    }

}