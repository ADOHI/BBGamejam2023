using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBGamejam.Ingame.Skill
{
    public class Skill : MonoBehaviour
    {
        public int level;
        public int maxLevel;
        public string skillName;
        public string skillDescription;
        public SkillManager.SkillType SkillType;

        public virtual void LevelUp()
        {
            level++;
        }

        public virtual void UpdateSkill()
        {

        }
    }
}
