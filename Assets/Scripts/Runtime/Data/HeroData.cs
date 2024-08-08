using System;

namespace Runtime.Data
{
    [Serializable]
    public class HeroData : UnitData
    {
        public int bulletAmount;
        public HeroType heroType;
        public BulletType bulletType;
    }
}