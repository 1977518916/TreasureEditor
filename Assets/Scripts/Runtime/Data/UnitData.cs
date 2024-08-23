using System;
namespace Runtime.Data
{
    [Serializable]
    public class UnitData
    {
        public int hp = 20;
        public int atk = 5;
        /// <summary>
        /// 是否无敌
        /// </summary>
        public bool isInvincible;
    }
}
