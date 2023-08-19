using Cysharp.Threading.Tasks;
using DG.Tweening;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BBGamejam.Global
{
    public class GlobalUIManager : Singleton<GlobalUIManager>
    {
        public enum FadeImageType
        {
            Black,
            White
        }

        private Image blackFadeImage;
        private Image whiteFadeImage;

        public void FadeIn(float duration, FadeImageType type, Ease ease = Ease.Linear)
        {
            switch (type)
            {
                case FadeImageType.Black:
                    blackFadeImage.DOFade(0f, duration).SetEase(ease);
                    break;
                case FadeImageType.White:
                    whiteFadeImage.DOFade(0f, duration).SetEase(ease);
                    break;
            }
        }

        public async UniTask FadeInAsync(float duration, FadeImageType type, Ease ease = Ease.Linear)
        {
            switch (type)
            {
                case FadeImageType.Black:
                    await blackFadeImage.DOFade(0f, duration).SetEase(ease);
                    break;
                case FadeImageType.White:
                    await whiteFadeImage.DOFade(0f, duration).SetEase(ease);
                    break;
            }
        }

        public void FadeOut(float duration, FadeImageType type, Ease ease = Ease.Linear)
        {
            switch (type)
            {
                case FadeImageType.Black:
                    blackFadeImage.DOFade(1f, duration).SetEase(ease);
                    break;
                case FadeImageType.White:
                    whiteFadeImage.DOFade(1f, duration).SetEase(ease);
                    break;
            }
        }

        public async UniTask FadeOutAsync(float duration, FadeImageType type, Ease ease = Ease.Linear)
        {
            switch (type)
            {
                case FadeImageType.Black:
                    await blackFadeImage.DOFade(1f, duration).SetEase(ease);
                    break;
                case FadeImageType.White:
                    await whiteFadeImage.DOFade(1f, duration).SetEase(ease);
                    break;
            }
        }
    }

}
