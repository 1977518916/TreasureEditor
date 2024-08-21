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

        /// <summary>
        /// 地图类型
        /// </summary>
        public MapTypeEnum mapType;
    }

    [Serializable]
    public class TimesData
    {
        /// <summary>
        /// 单波敌人数量
        /// </summary>
        public int amount;
        /// <summary>
        /// 当前波出怪间隔
        /// </summary>
        public float time = 10;
        /// <summary>
        /// 每次生成时的间隔
        /// </summary>
        public float makeTime = 1;

        /// <summary>
        /// 敌人的生命值攻击力
        /// </summary>
        public UnitData enemyData = new UnitData();
        
        public EnemyTypeEnum enemyType = EnemyTypeEnum.XiaoBing;
    }
}