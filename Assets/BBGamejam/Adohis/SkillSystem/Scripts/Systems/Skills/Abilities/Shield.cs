using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Ingame.Ability
{
    public class Shield : MonoBehaviour
    {
        public float regenInterval = 10f;


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public string MakeDescription()
        {
            var description = $"피해를 {regenInterval}초 동안 받지 않을 시 다음 피해를 막습니다";
            return description;
        }
    }

}
