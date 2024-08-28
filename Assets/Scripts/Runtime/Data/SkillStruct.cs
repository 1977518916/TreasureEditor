using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data
{
    public class SkillStruct : ScriptableObject
    {
        /// <summary>
        /// 对应
        /// </summary>
        public Dictionary<string, SkillData> SkillDatas;
    }

    [Serializable]
    public class SkillData
    {
        
    }
}