using System;
using System.Collections.Generic;
using System.Linq;
using QFSW.QC;
using Runtime.Data;
using Sirenix.Utilities;
using Spine.Unity;
using Tao_Framework.Core.Event;
using UnityEngine;

namespace Runtime.Manager
{
    public static class DataManager
    {
        /// <summary>
        /// 本次使用的英雄列表
        /// </summary>
        private static List<HeroData> heroDataList = new List<HeroData>();

        /// <summary>
        /// 所有的动画资源
        /// </summary>
        private static readonly Dictionary<EntityModelType, List<SkeletonDataAsset>> AllSpineDic =
            new Dictionary<EntityModelType, List<SkeletonDataAsset>>();

        /// <summary>
        /// 所有实体攻击动画字典
        /// </summary>
        private static readonly Dictionary<EntityModelType, SkeletonDataAsset> AllEntityAttackSpineDic =
            new Dictionary<EntityModelType, SkeletonDataAsset>();

        public static readonly Dictionary<string, SkeletonDataAsset> AllEntitySkillSpineDic =
            new Dictionary<string, SkeletonDataAsset>();

        /// <summary>
        /// 技能数据
        /// </summary>
        public static SkillStruct SkillStruct { get; private set; }
        
        /// <summary>
        /// 实体寻常动画
        /// </summary>
        private static readonly Dictionary<EntityModelType, SkeletonDataAsset> EntityCommonSpineDic =
            new Dictionary<EntityModelType, SkeletonDataAsset>();

        /// <summary>
        /// 关卡数据
        /// </summary>
        private static LevelData levelData = new LevelData();

        /// <summary>
        /// 游戏内的动态数据
        /// </summary>
        private static GameData gameData = new GameData();

        /// <summary>
        /// 地图素材路径
        /// </summary>
        public const string MapTexturePath = "Texture/LongMap/Map_";

        /// <summary>
        /// 预制体路径
        /// </summary>
        public const string PrefabPath = "Prefabs/";

        public static void Init(Action action)
        {
            ES3.Init();
            InitReadData();
            InitAllSpineData();
            SkillStruct = AssetsLoadManager.Load<SkillStruct>("Config/AllSkillData");
            action.Invoke();
        }

        /// <summary>
        /// 初始化读取数据
        /// </summary>
        private static void InitReadData()
        {
            IsNotData();
            heroDataList = ReadHeroData();
            levelData = ReadLevelData();
            gameData = ReadRuntimeData();
        }
        
        /// <summary>
        /// 如果一开始没有存档的数据就保存一次
        /// </summary>
        private static void IsNotData()
        {
            if (!ES3.KeyExists(HelpTools.GetEnumValueName<DataType>(DataType.HeroDataList)))
                SaveHeroData();
            if (!ES3.KeyExists(HelpTools.GetEnumValueName<DataType>(DataType.LevelData)))
                SaveLevelData();
            if (!ES3.KeyExists(HelpTools.GetEnumValueName<DataType>(DataType.RuntimeData)))
                SaveRuntimeData();
        }

        // Spine相关API
        #region Spine

        /// <summary>
        /// 获取指定实体的子弹动画文件
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="asset"></param>
        /// <returns></returns>
        public static bool GetSpecifyEntityBulletSpine(EntityModelType modelType, out SkeletonDataAsset asset)
        {
            return AllEntityAttackSpineDic.TryGetValue(modelType, out asset);
        }

        /// <summary>
        /// 获取指定实体的寻常动画
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="asset"></param>
        /// <returns></returns>
        public static bool GetSpecifyEntityCommonSpine(EntityModelType modelType, out SkeletonDataAsset asset)
        {
            return EntityCommonSpineDic.TryGetValue(modelType, out asset);
        }

        /// <summary>
        /// 初始化所有动画文件数据
        /// </summary>
        private static void InitAllSpineData()
        {
            var allEffect = Resources.LoadAll<SkeletonDataAsset>("");
            foreach (EntityModelType entityName in Enum.GetValues(typeof(EntityModelType)))
            {
                SpineClassify(entityName, allEffect);
            }

            foreach (var data in AllSpineDic)
            {
                InitAllEntityAttackSpine(data.Key, data.Value);
                InitAllEntityCommonSpine(data.Key, data.Value);
            }

            InitAllEntitySkillSpine(allEffect);


            EventMgr.Instance.TriggerEvent(GameEvent.DataInitEnd, 0.15f);
        }

        /// <summary>
        /// 动画文件根据角色分类
        /// </summary>
        private static void SpineClassify(EntityModelType type, IEnumerable<SkeletonDataAsset> dataList)
        {
            var skeletonDataList = dataList
                .Where(dataAsset => dataAsset.name.ToLower().Contains(type.ToString().ToLower())).ToList();
            if (AllSpineDic.TryAdd(type, skeletonDataList)) return;
            AllSpineDic[type] = null;
            AllSpineDic[type] = skeletonDataList;
        }

        /// <summary>
        /// 初始化所有实体的寻常动画
        /// </summary>
        /// <param name="type"></param>
        /// <param name="entitySpine"></param>
        private static void InitAllEntityCommonSpine(EntityModelType type, List<SkeletonDataAsset> entitySpine)
        {
            foreach (var data in entitySpine.Where(data =>
                         data.name.ToLower().Contains(type.ToString().ToLower()) &&
                         !data.name.ToLower().Contains("attack") &&
                         !data.name.ToLower().Contains("hit") &&
                         !data.name.ToLower().Contains("skill")))
            {
                if (EntityCommonSpineDic.TryAdd(type, data))
                    return;
                EntityCommonSpineDic[type] = data;
            }
        }

        /// <summary>
        /// 分类对应实体的攻击动画文件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="entitySpine"></param>
        private static void InitAllEntityAttackSpine(EntityModelType type, List<SkeletonDataAsset> entitySpine)
        {
            foreach (var data in entitySpine.Where(data => data.name.ToLower().Contains(type.ToString().ToLower()) &&
                                                           data.name.ToLower().Contains("attack")))
            {
                if (AllEntityAttackSpineDic.TryAdd(type, data))
                    return;
                AllEntityAttackSpineDic[type] = data;
            }
        }

        /// <summary>
        /// 对应实体是否拥有子弹
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool EntityIsHaveBullet(EntityModelType type)
        {
            return AllEntityAttackSpineDic.ContainsKey(type);
        }

        private static void InitAllEntitySkillSpine(SkeletonDataAsset[] allEffect)
        {
            foreach (SkeletonDataAsset asset in allEffect.Where(data => data.name.Contains("skill")))
            {
                AllEntitySkillSpineDic.Add(asset.name, asset);
            }
        }

        /// <summary>
        /// 获取所有实体的寻常动画
        /// </summary>
        /// <returns></returns>
        public static Dictionary<EntityModelType, SkeletonDataAsset> GetAllEntityCommonSpine()
        {
            return EntityCommonSpineDic;
        }

        #endregion
        
        // 数据存储和读取相关的API
        #region DataSaveAndRead
        
        /// <summary>
        /// 保存英雄数据
        /// </summary>
        public static void SaveHeroData()
        {
            ES3.Save(HelpTools.GetEnumValueName<DataType>(DataType.HeroDataList), heroDataList);
        }

        /// <summary>
        /// 读取英雄数据
        /// </summary>
        /// <returns></returns>
        private static List<HeroData> ReadHeroData()
        {
            var value = ES3.Load<List<HeroData>>(HelpTools.GetEnumValueName<DataType>(DataType.HeroDataList));
            return value;
        }

        /// <summary>
        /// 保存关卡数据
        /// </summary>
        public static void SaveLevelData()
        {
            ES3.Save(HelpTools.GetEnumValueName<DataType>(DataType.LevelData), levelData);
        }   
        
        /// <summary>
        /// 读取关卡数据
        /// </summary>
        /// <returns></returns>
        private static LevelData ReadLevelData()
        {
            return ES3.Load<LevelData>(HelpTools.GetEnumValueName<DataType>(DataType.LevelData));
        }
        
        /// <summary>
        /// 保存运行时数据
        /// </summary>
        public static void SaveRuntimeData()
        {
            ES3.Save(HelpTools.GetEnumValueName<DataType>(DataType.RuntimeData), gameData);
        }
        
        /// <summary>
        /// 读取运行时数据
        /// </summary>
        /// <returns></returns>
        private static GameData ReadRuntimeData()
        {
            return ES3.Load<GameData>(HelpTools.GetEnumValueName<DataType>(DataType.RuntimeData));
        }

        #endregion
        
        // 获取对应数据的接口
        #region Get
        
        /// <summary>
        /// 获取英雄数据列表
        /// </summary>
        /// <returns></returns>
        public static List<HeroData> GetHeroDataList()
        {
            return heroDataList;
        }
        
        /// <summary>
        /// 获取关卡数据
        /// </summary>
        /// <returns></returns>
        public static LevelData GetLevelData()
        {
            return levelData;
        }
        
        /// <summary>
        /// 获取运行时数据
        /// </summary>
        /// <returns></returns>
        public static GameData GetRuntimeData()
        {
            return gameData;
        }

        #endregion
        
        // 设置对应数据的接口
        #region Set
        
        /// <summary>
        /// 设置英雄列表数据
        /// </summary>
        /// <param name="data"></param>
        public static void SetHeroListData(List<HeroData> data)
        {
            heroDataList.Clear();
            heroDataList.AddRange(data);
        }
        
        /// <summary>
        /// 设置关卡数据
        /// </summary>
        /// <param name="data"></param>
        public static void SetLevelData(LevelData data)
        {
            levelData = data;
        }

        /// <summary>
        /// 设置运行时数据
        /// </summary>
        /// <param name="data"></param>
        public static void SetRuntimeData(GameData data)
        {
            gameData = data;
        }

        #endregion
    }
}