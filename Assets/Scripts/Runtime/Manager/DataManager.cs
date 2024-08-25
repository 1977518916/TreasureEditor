using System;
using System.Collections.Generic;
using QFSW.QC;
using Runtime.Data;
using Spine.Unity;
using UnityEngine;

namespace Runtime.Manager
{
    public static class DataManager
    {
        /// <summary>
        /// 本次使用的英雄列表
        /// </summary>
        public static readonly Dictionary<DataType.HeroPositionType, HeroData> HeroDatas =
            new Dictionary<DataType.HeroPositionType, HeroData>();
        
        /// <summary>
        /// 所有的特效资源
        /// </summary>
        private static readonly Dictionary<BossType, List<SkeletonDataAsset>> AllEffectDic =
            new Dictionary<BossType, List<SkeletonDataAsset>>();
        
        /// <summary>
        /// 关卡数据
        /// </summary>
        public static LevelData LevelData;
        /// <summary>
        /// 游戏内的动态数据
        /// </summary>
        public static GameData GameData;
        /// <summary>
        /// 获取当前使用的英雄列表游戏物体
        /// </summary>
        /// <returns></returns>
        [Command]
        public static List<GameObject> GetHeroes()
        {
            List<GameObject> gameObjects = new List<GameObject>();
            foreach ((DataType.HeroPositionType key, HeroData value) in HeroDatas)
            {
                if(value.heroTypeEnum == HeroTypeEnum.Null)
                {
                    gameObjects.Add(null);
                    continue;
                }
                gameObjects.Add(AssetsLoadManager.LoadHero(value.heroTypeEnum, null));
            }
            return gameObjects;
        }

        /// <summary>
        /// 地图素材路径
        /// </summary>
        public const string MapTexturePath = "Texture/LongMap/Map_";

        /// <summary>
        /// 预制体路径
        /// </summary>
        public const string PrefabPath = "Prefabs/";

        /// <summary>
        /// 获取指定实体的所有特效动画相关资源文件
        /// </summary>
        /// <returns></returns>
        public static List<SkeletonDataAsset> GetSpecifyEntityEffect(Enum entityEnum)
        {
            var name = entityEnum.ToString();
            return default;
        }

        /// <summary>
        /// 初始化所有特效数据
        /// </summary>
        private static void InitAllEffectData()
        {
            var allEffect = Resources.LoadAll<SkeletonDataAsset>("Effect/Spine");
            foreach (var entityName in Enum.GetNames(typeof(BossType)))
            {
                
            }
        }
    }
}