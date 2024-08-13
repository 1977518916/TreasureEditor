using System;
using System.Collections.Generic;

namespace Runtime.Data
{
    [Serializable]
    public class LevelData
    {
        public List<TimesData> timesDatas = new List<TimesData>()
        {
            new TimesData()
        };
    }

    [Serializable]
    public class TimesData
    {
        public int amount;
        public EnemyTypeEnum enemyType = EnemyTypeEnum.XiaoBing;
    }
}