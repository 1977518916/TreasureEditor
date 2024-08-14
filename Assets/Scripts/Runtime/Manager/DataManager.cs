using System.Collections.Generic;
using Runtime.Data;

namespace Runtime.Manager
{
    public static class DataManager
    {
        public static readonly Dictionary<DataType.HeroPositionType, HeroData> HeroDatas = new Dictionary<DataType.HeroPositionType, HeroData>();

        /// <summary>
        /// 地图素材路径
        /// </summary>
        public const string MapTexturePath = "Texture/LongMap/Map_";
    }
}