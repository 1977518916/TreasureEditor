using System;
using System.Data;
using Runtime.Data;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using File = System.IO.File;
using Object = UnityEngine.Object;

namespace Runtime.Manager
{
    public static class AssetsLoadManager
    {
        private const string CharacterPath = "Character/";
        private const string EffectPath = "Effect/Spine/";
        private const string ExternalPath = "/External/";
        public static GameObject LoadHero(EntityModelType heroTypeEnum, Transform parent)
        {
            return LoadEntityModelSkeleton(heroTypeEnum, parent, StateType.Idle).GameObject();
        }

        public static GameObject LoadEnemy(EnemyTypeEnum enemyTypeEnum, Transform parent)
        {
            return LoadCharacterSkeletonOfEnum(enemyTypeEnum, parent).GameObject();
        }
        
        public static GameObject LoadBoss(EntityModelType entityModelType, Transform parent)
        {
            return LoadEntityModelSkeleton(entityModelType, parent, StateType.Idle).GameObject();
        }
        
        public static BulletEntity LoadBullet(EntityModelType entityModelType, Transform parent = null)
        {
            var gameObject = new GameObject($"{entityModelType.ToString()}_Bullet", typeof(RectTransform))
            {
                layer = 5
            };
            gameObject.transform.SetParent(parent);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localScale = Vector3.one;
            var bulletEntity = gameObject.AddComponent<BulletEntity>();
            bulletEntity.MoveObject = LoadBulletSkeletonOfEnum(entityModelType, gameObject.transform).GameObject();
            bulletEntity.MoveObject.transform.eulerAngles = new Vector3(0, 0, -90);
            return bulletEntity;
        }

        public static Sprite LoadBg(MapTypeEnum mapTypeEnum)
        {
            if(mapTypeEnum != MapTypeEnum.Other)
            {
                return Resources.Load<Sprite>(DataManager.MapTexturePath + (int)mapTypeEnum);
            }
            byte[] fileData = LoadExternalMap();
            Texture2D texture2D = new Texture2D(2, 2);
            texture2D.filterMode = FilterMode.Bilinear;
            if(texture2D.LoadImage(fileData))
            {
                return Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
            }
            throw new DataException("外部地图数据读取错误!");

        }

        private static byte[] LoadExternalMap()
        {
            string path = Application.dataPath + ExternalPath + "map.png";
            if(File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            path = Application.dataPath + ExternalPath + "map.jpg";    
            if(File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            throw new DataException("未找到外部地图!");
        }

        public static SkeletonGraphic LoadSkeletonGraphic(string path, Transform parent = null)
        {
            SkeletonDataAsset asset = Load<SkeletonDataAsset>(path);
            SkeletonGraphic skeletonGraphic = SkeletonGraphic.NewSkeletonGraphicGameObject(asset, parent, Graphic.defaultGraphicMaterial);
            return skeletonGraphic;
        }

        public static SkeletonGraphic LoadSkeletonGraphic(EntityModelType type, Transform parent = null)
        {
            if (DataManager.GetSpecifyEntityCommonSpine(type, out var asset))
            {

                return SkeletonGraphic.NewSkeletonGraphicGameObject(asset, parent, Graphic.defaultGraphicMaterial);
            }
            else
            {
                throw new Exception($"没有这个模型对应的寻常动画文件,请检查： {type}");
            }
        }

        public static SkeletonGraphic LoadSkeletonGraphic(SkeletonDataAsset asset, Transform parent = null)
        {
            SkeletonGraphic skeletonGraphic =
                SkeletonGraphic.NewSkeletonGraphicGameObject(asset, parent, Load<Material>("Material/SkeletonGraphicDefault"));
            skeletonGraphic.AnimationState.SetAnimation(0, skeletonGraphic.SkeletonData.Animations.Items[0].Name,
                false);
            return skeletonGraphic;
        }

        private static SkeletonGraphic LoadCharacterSkeletonOfEnum(Enum @enum, Transform parent)
        {
            string path = CharacterPath + $"{@enum.ToString()}/{@enum.ToString()}_skeletondata";
            SkeletonGraphic skeletonAnimation = LoadSkeletonGraphic(path, parent);
            skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
            skeletonAnimation.name = @enum.ToString();
            return skeletonAnimation;
        }
        
        /// <summary>
        /// 加载实体模型 并播放传入的动画类型
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="parent"></param>
        /// <param name="stateType"></param>
        /// <returns></returns>
        private static SkeletonGraphic LoadEntityModelSkeleton(EntityModelType modelType, Transform parent, StateType stateType)
        {
            var skeletonAnima = LoadSkeletonGraphic(modelType, parent);
            skeletonAnima.AnimationState.SetAnimation(0, stateType.ToString(), true);
            skeletonAnima.name = $"{modelType}_Model";
            return skeletonAnima;
        }

        private static SkeletonGraphic LoadBulletSkeletonOfEnum(Enum @enum, string path, Transform parent = null)
        {
            string p = EffectPath + path;
            SkeletonGraphic skeletonAnimation = LoadSkeletonGraphic(p, parent);
            skeletonAnimation.name = @enum.ToString();
            skeletonAnimation.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            return skeletonAnimation;
        }
        
        public static SkeletonGraphic LoadBulletSkeletonOfEnum(EntityModelType entityModelType, Transform parent = null)
        {
            if (!DataManager.GetSpecifyEntityBulletSpine(entityModelType, out var dataAsset)) return null;
            var anima = LoadSkeletonGraphic(dataAsset, parent);
            anima.name = entityModelType.ToString();
            anima.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            return anima;
        }

        public static T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}