using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Data
{
    [CreateAssetMenu(menuName = "Runtime/Data/SkillStruct")]
    public class SkillStruct : ScriptableObject
    {
        /// <summary>
        /// 对应
        /// </summary>
        public List<SkillData> skillDatas;

        public SkillData GetSkillDataOfKey(string key)
        {
            foreach (SkillData skillData in skillDatas)
            {
                if(skillData.key.Equals(key))
                {
                    return skillData;
                }
            }
            return null;
        }
    }

    [Serializable]
    public class SkillData
    {
        [Space(10)]
        [Tooltip("绑定的技能表现层")]
        public string key = "skill_";
        [Tooltip("技能运动类型")]
        public SkillMoveType skillMoveType = SkillMoveType.Bullet;
        [Tooltip("技能释放的量")]
        public int amount = 1;
        [Tooltip("ui展示时的缩放大小")]
        public float showScale = 0.5f;
        [Tooltip("展示时位置的偏移")]
        public Vector2 showPosition = Vector2.zero;
        [Tooltip("游戏中技能需要旋转的角度")]
        public float rotations = 0;
        [Tooltip("是否持续播放动画")]
        public bool isLoopPlay = true;
        [ShowIf("HasDamageDelay")]
        [Tooltip("伤害触发的延迟")]
        public float damageDelay = 0;
        [ShowIf("HasRange")]
        [Tooltip("伤害的范围")]
        public float damageRange = 0;

        private bool HasDamageDelay()
        {
            return skillMoveType switch
            {
                SkillMoveType.DelayRange => true,
                _ => false
            };
        }

        private bool HasRange()
        {
            return skillMoveType switch
            {
                SkillMoveType.DelayRange => true,
                SkillMoveType.HideRange => true,
                _ => false
            };
        }
    }

    /// <summary>
    /// 技能类型
    /// </summary>
    public enum SkillEffectType
    {
        Cure,
        Attack,
        Buff,
    }
    /// <summary>
    /// 技能的表现类型
    /// </summary>
    public enum SkillMoveType
    {
        [InspectorName("子弹移动")]
        Bullet,
        [InspectorName("延迟范围伤害")]
        DelayRange,
        [InspectorName("用作自身不移动")]
        Self,
        [InspectorName("贯穿")]
        Through,
        [InspectorName("自身消失，并造成范围伤害")]
        HideRange,
    }
}