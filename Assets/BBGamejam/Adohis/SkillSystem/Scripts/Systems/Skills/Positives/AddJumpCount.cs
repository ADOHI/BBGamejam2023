using BBGamejam.Global.Ingame;
using RabbitResurrection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Ingame.Skill
{
    public class AddJumpCount : Skill
    {
        public override void LevelUp()
        {
            base.LevelUp();
            UpdateSkill();
        }

        public override void UpdateSkill()
        {
            var rabbit = IngameManager.Instance.rabbit.Value.GetComponent<Rabbit>();

            rabbit.AddMaxAir(1);
        }
    }

}
