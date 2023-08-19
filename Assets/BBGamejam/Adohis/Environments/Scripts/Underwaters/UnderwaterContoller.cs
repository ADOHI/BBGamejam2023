using AtmosphericHeightFog;
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

        [Header("ValuesForSet")]
        public FloatReference depthOfWater;
        public FloatReference maxDepthOfWater;
        public FloatReference gameProgress;

        void Update()
        {
            SetFogPosition();
            SetWaterConfig(depthOfWater);
        }

        private void SetFogPosition()
        {
            waterFog.transform.position = characterObject.Value.transform.position;
        }

        private void SetWaterConfig(float depthOfWater)
        {
            waterFog.fogColorDuo = Mathf.Clamp(gameProgress.Value, minFogColorDuo, maxFogColorDuo);
        }
    }
}

