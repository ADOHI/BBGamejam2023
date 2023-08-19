using AtmosphericHeightFog;
using BBGamejam.Global.Ingame;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace BBGamejam.Envirionment.Underwater
{
    public class UnderwaterContoller : MonoBehaviour
    {
        public GameObjectReference characterObject;
        [Header("AssignFog")]
        public HeightFogGlobal waterFog;
        public float minFogColorDuo;
        public float maxFogColorDuo;

        void Update()
        {
            SetFogPosition();
            SetWaterConfig();
        }

        private void SetFogPosition()
        {
            waterFog.transform.position = characterObject.Value.transform.position;
        }

        private void SetWaterConfig()
        {
            waterFog.fogColorDuo = Mathf.Clamp(IngameManager.Instance.progress, minFogColorDuo, maxFogColorDuo);
        }
    }
}

