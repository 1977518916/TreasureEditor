using System;
using System.Collections.Generic;
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
        [Tooltip("绑定的技能表现层")]
        public string key;
        [Tooltip("技能效果")]
        public SkillEffectType skillEffectType = SkillEffectType.Attack;
        [Tooltip("技能运动类型")]
        public SkillMoveType skillMoveType = SkillMoveType.Bullet;
        [Tooltip("技能释放的量")]
        public int amount = 1;
        [Tooltip("ui展示时的缩放大小")]
        public float showScale;
        [Tooltip("展示时位置的偏移")]
        public Vector2 showPosition;
    }

    /// <summary>
    /// 技能类型
    /// </summary>
    public enum SkillEffectType
    {
        Cure,
        Attack,

    }
    /// <summary>
    /// 技能的表现类型
    /// </summary>
    public enum SkillMoveType
    {
        Bullet,

    }
}