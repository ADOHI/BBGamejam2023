using BBGamejam.Global.Ingame;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BBGamejam.Ingame.UI
{
    public class IngameUIManager : Singleton<IngameUIManager>
    {
        private TextMeshProUGUI comboText;

        public TextMeshProUGUI depthOfWaterText;
        public TextMeshProUGUI scoreText;
        public GameObject comboTextCanvas;
        public TextMeshProUGUI comboTextPrefab;
        public GameObject finalComboTextCanvas;
        public TextMeshProUGUI finalComboTextPrefab;

        private void Update()
        {
            UpdateIngameUI();
        }

        private void UpdateIngameUI()
        {
            depthOfWaterText.text = $"{(int)(IngameManager.Instance.progress * 2077f)} m";
            scoreText.text = IngameManager.Instance.score.Value.ToString();
/*            comboText.transform.position = 
                Camera.main.WorldToScreenPoint(IngameManager.Instance.rabbit.Value.transform.position);
            comboText.text = $"{IngameManager.Instance.currentComboCount} Combo";*/
        }

        public void SpawnComboTextAsync(Vector3 spawnPosition, Vector3 offset, float duration, Ease ease = Ease.Linear)
        {
            var comboTextObject = Instantiate(comboTextPrefab);
            comboTextObject.transform.SetParent(comboTextCanvas.transform);
            
            var fromPosition = Camera.main.WorldToScreenPoint(spawnPosition);

            comboTextObject.transform.position = fromPosition;
            comboTextObject.text = IngameManager.Instance.currentComboCount.ToString();
            comboTextObject.gameObject.SetActive(true);

            var toPosition = Camera.main.WorldToScreenPoint(spawnPosition + offset);
            var scale = 1f + IngameManager.Instance.currentComboCount / 5f;
            var fromScale = 2f * scale;
            comboTextObject.transform.DOScale(scale, duration / 10f).From(fromScale).SetEase(ease);
            comboTextObject.transform.DOMove(toPosition, duration).SetEase(ease);
            comboTextObject.DOFade(0f, duration).SetEase(ease).OnComplete(() => Destroy(comboTextObject.gameObject));
        }

        public async UniTask SpawnFinalComboTextAsync(Vector3 fromOffset, Vector3 toOffset, float duration, Ease ease = Ease.Linear)
        {
            var spawnedComboText = Instantiate(finalComboTextPrefab);
            comboText = spawnedComboText;
            spawnedComboText.transform.SetParent(finalComboTextCanvas.transform);
            var comboCount = IngameManager.Instance.currentComboCount;
            spawnedComboText.text = $"{comboCount} Combo! <size=36>({IngameManager.Instance.BasicScore}+{IngameManager.Instance.BonusScore})</size>";
            spawnedComboText.rectTransform.anchoredPosition = Vector3.zero + fromOffset;
            var scale = 1f + comboCount / 10f;
            spawnedComboText.transform.localScale = Vector3.one * scale;
            spawnedComboText.gameObject.SetActive(true);
            var fromPosition = spawnedComboText.transform.position;
            var toPosition = fromPosition + toOffset;
            var fromScale = 2f * (scale);
            await spawnedComboText.transform.DOScale(1f, duration / 5f).From(fromScale).SetEase(ease);
            await UniTask.Delay((int)(1000 * duration));
            spawnedComboText.transform.DOMove(toPosition, duration / 5f).SetEase(ease);
            spawnedComboText
                .DOFade(0f, duration / 5f)
                .SetEase(ease)
                .OnComplete(() =>
                {
                    Destroy(spawnedComboText.gameObject);
                });
        }

        public void HideComboText()
        {
            if (comboText != null)
            {
                comboText.gameObject.SetActive(false);
            }
        }
    }

}
