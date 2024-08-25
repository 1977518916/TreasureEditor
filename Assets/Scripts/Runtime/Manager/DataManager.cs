using System;
using System.Collections.Generic;
using System.Linq;
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
        /// 所有的动画资源
        /// </summary>
        private static readonly Dictionary<BossType, List<SkeletonDataAsset>> AllSpineDic =
            new Dictionary<BossType, List<SkeletonDataAsset>>();
        
        /// <summary>
        /// 所有实体攻击动画字典
        /// </summary>
        private static readonly Dictionary<BossType, SkeletonDataAsset> AllEntityAttackDic =
            new Dictionary<BossType, SkeletonDataAsset>();
        
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
            if (AllSpineDic.Count == 0)
                InitAllEffectData();
            return default;
        }

        /// <summary>
        /// 初始化所有动画文件数据
        /// </summary>
        private static void InitAllEffectData()
        {
            var allEffect = Resources.LoadAll<SkeletonDataAsset>("");
            foreach (BossType entityName in Enum.GetValues(typeof(BossType)))
            {
                EffectClassify(entityName, allEffect);
            }

            foreach (var data in AllSpineDic)
            {
                InitAllEntityAttackSpine(data.Key, data.Value);
            }

            Debug.Log(AllEntityAttackDic);
        }

        /// <summary>
        /// 特效文件根据角色分类
        /// </summary>
        private static void EffectClassify(BossType type, IEnumerable<SkeletonDataAsset> dataList)
        {
            var skeletonDataList = dataList.Where(dataAsset => dataAsset.name.ToLower().Contains(type.ToString().ToLower())).ToList();
            if (AllSpineDic.TryAdd(type, skeletonDataList)) return;
            AllSpineDic[type] = null;
            AllSpineDic[type] = skeletonDataList;
        }
        
        /// <summary>
        /// 分类对应实体的攻击动画文件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="entitySpine"></param>
        private static void InitAllEntityAttackSpine(BossType type, List<SkeletonDataAsset> entitySpine)
        {
            foreach (var data in entitySpine.Where(data => data.name.ToLower().Contains(type.ToString().ToLower()) &&
                                                           data.name.ToLower().Contains("attack")))
            {
                if (AllEntityAttackDic.TryAdd(type, data))
                    return;
                AllEntityAttackDic[type] = data;
            }
        }
    }
}