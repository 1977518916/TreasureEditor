using System;
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

        public EntityModelType modelType = EntityModelType.Null;
        public BulletType bulletType = BulletType.Self;
        public BulletAttributeType bulletAttributeType = BulletAttributeType.Penetrate;
        public BulletMoveType bulletMoveType = BulletMoveType.RectilinearMotion;

        /// <summary>
        /// 攻击间隔
        /// </summary>
        public float atkInterval = 2;

        /// <summary>
        /// 模型大小
        /// </summary>
        public float modelScale = 1;

        public SkillData skillData1;

        public SkillData skillData2;
    }
}