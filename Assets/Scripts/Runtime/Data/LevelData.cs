using System;
using System.Collections.Generic;

namespace Runtime.Data
{
    [Serializable]
    public class LevelData
    {
        /// <summary>
        /// 总波次的集合
        /// </summary>
        public List<TimesData> timesDatas = new List<TimesData>()
        {
            new TimesData()
        };
    }

    [Serializable]
    public class TimesData
    {
        /// <summary>
        /// 单波敌人数量
        /// </summary>
        public int amount;
        public EnemyTypeEnum enemyType = EnemyTypeEnum.XiaoBing;
    }
}