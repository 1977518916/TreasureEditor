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
    }

    [Serializable]
    public class SkillData
    {
        [Tooltip("绑定的技能表现层")]
        public string key;
        [Tooltip("技能效果")]
        public SkillEffectType skillEffectType;
        [Tooltip("技能运动类型")]
        public SkillMoveType skillMoveType;
        [Tooltip("技能释放的量")]
        public int amount = 1;
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