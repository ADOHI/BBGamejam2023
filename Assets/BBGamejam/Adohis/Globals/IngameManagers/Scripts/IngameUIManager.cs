using BBGamejam.Global.Ingame;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BBGamejam.Ingame.UI
{
    public class IngameUIManager : Singleton<IngameUIManager>
    {
        public TextMeshProUGUI depthOfWater;

        private void Update()
        {
            UpdateIngameUI();
        }

        private void UpdateIngameUI()
        {
            depthOfWater.text = $"{(int)(IngameManager.Instance.progress * 2077f)} m";
        }
    }

}
