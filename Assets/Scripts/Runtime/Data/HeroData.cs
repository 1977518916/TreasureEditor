using System;
using UnityEngine.Serialization;

namespace Runtime.Data
{
    [Serializable]
    public class HeroData : UnitData
    {
        /// <summary>
        /// 子弹数量
        /// </summary>
        public int bulletAmount;
        [FormerlySerializedAs("heroType")]
        public HeroTypeEnum heroTypeEnum = HeroTypeEnum.Null;
        public BulletType bulletType;
    }
}