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
            var description = $"���ظ� {regenInterval}�� ���� ���� ���� �� ���� ���ظ� �����ϴ�";
            return description;
        }
    }

}
