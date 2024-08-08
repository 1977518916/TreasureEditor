using System;
using System.Collections.Generic;

namespace Runtime.Data
{
    [Serializable]
    public class LevelData
    {
        public List<TimesData> timesDatas;
    }

    [Serializable]
    public class TimesData
    {
        public int amount;
        public int enemyType;
    }
}