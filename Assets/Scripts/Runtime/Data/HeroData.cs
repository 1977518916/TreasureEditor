using System;
using UnityEngine.Serialization;

namespace Runtime.Data
{
    [Serializable]
    public class HeroData : UnitData
    {
        public int bulletAmount;
        [FormerlySerializedAs("heroType")]
        public HeroTypeEnum heroTypeEnum;
        public BulletType bulletType;
    }
}