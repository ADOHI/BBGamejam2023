using BBGamejam.Global.Ingame;
using RabbitResurrection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Ingame.Skill
{
    public class AddJumpPower : Skill
    {
        public float powerMultply = 1.1f;
        public override void LevelUp()
        {
            base.LevelUp();
            UpdateSkill();
        }

        public override void UpdateSkill()
        {
            var swipe = IngameManager.Instance.rabbit.Value.GetComponent<Swipe>();

            swipe.maxForce *= powerMultply;
        }
    }

}
