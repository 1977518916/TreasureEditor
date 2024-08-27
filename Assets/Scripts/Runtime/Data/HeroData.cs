﻿using System;
using UnityEngine.Serialization;

namespace Runtime.Data
{
    [Serializable]
    public class HeroData : UnitData
    {
        /// <summary>
        /// 弹夹数量
        /// </summary>
        public int bulletAmount = 10;
        /// <summary>
        /// 每次射出的子弹数量
        /// </summary>
        public int shooterAmount = 1;
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