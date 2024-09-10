using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data
{
    [Serializable]
    public class LevelData
    {
        /// <summary>
        /// 总波次的集合
        /// </summary>
        public List<EnemyMakerData> EnemyMakerDatas = new List<EnemyMakerData>()
        {
            new EnemyMakerData()
        };
        
        /// <summary>
        /// 地图精灵
        /// </summary>
        public int mapIndex;

        /// <summary>
        /// Boss的数据 目前整个关卡只支持单个Boss
        /// </summary>
        public BossData BossData;
    }

    [Serializable]
    public class EnemyMakerData
    {
        /// <summary>
        /// 单波敌人数量
        /// </summary>
        public int amount = 3;
        
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
        public EnemyData enemyData = new EnemyData();
        
        public EntityModelType enemyType = EntityModelType.XiaoBing;

        public EnemyType enemyActionType = EnemyType.Melee;
    }

    [Serializable]
    public class BossData
    {
        /// <summary>
        /// Boss 的模型
        /// </summary>
        public EntityModelType EntityModelType;

        /// <summary>
        /// Boss 的子弹 或 叫做攻击方式
        /// </summary>
        public BulletType BulletType;

        /// <summary>
        /// 生成时间
        /// </summary>
        public float Time = 30f;

        /// <summary>
        /// 攻击力
        /// </summary>
        public int Atk = 100;
        
        /// <summary>
        /// 血量
        /// </summary>
        public int Hp = 500;
        
        /// <summary>
        /// 模型大小
        /// </summary>
        public float modelScale = 1f;
        
        /// <summary>
        /// 移动速度
        /// </summary>
        public float RunSpeed = 30f;
    }
}