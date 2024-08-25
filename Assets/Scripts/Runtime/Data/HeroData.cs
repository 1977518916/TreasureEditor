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
        public int bulletAmount = 10;
        [FormerlySerializedAs("heroType")]
        public HeroTypeEnum heroTypeEnum = HeroTypeEnum.Null;
        public BulletType bulletType = BulletType.Self;
        public BulletMoveType bulletMoveType;
        /// <summary>
        /// 攻击间隔
        /// </summary>
        public float atkInterval = 2;
    }
}