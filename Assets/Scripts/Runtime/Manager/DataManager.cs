using System.Collections.Generic;
using Runtime.Data;

namespace Runtime.Manager
{
    public static class DataManager
    {
        public static readonly Dictionary<DataType.HeroPositionType, HeroData> HeroDatas = new Dictionary<DataType.HeroPositionType, HeroData>();
    }
}