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
    /// <summary>
    /// 仅负责资源加载，不负责实体生成相关逻辑
    /// </summary>
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

        /// <summary>
        /// 加载子弹
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="layer"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static GameObject LoadBullet(EntityModelType modelType, int layer, Transform parent = null)
        {
            var bulletObj = new GameObject($"{modelType.ToString()}_Bullet", typeof(RectTransform))
            {
                layer = layer
            };
            bulletObj.transform.SetParent(parent);
            bulletObj.transform.localPosition = Vector3.zero;
            bulletObj.transform.localScale = new Vector3(2f, 2f, 1f);
            if(HelpTools.BulletIsSpine(modelType))
            {
                var spine = LoadBulletSkeletonOfEnum(modelType, bulletObj.transform).GameObject();
                spine.transform.eulerAngles = new Vector3(0, 0, -90f);
            }
            else
            {
                var bullet = Load<GameObject>(HelpTools.BulletPrefabPath(modelType));
                bullet.transform.SetParent(bulletObj.transform);
                bullet.transform.eulerAngles = new Vector3(0, 0, 90f);
            }
            return bulletObj;
        }

        /// <summary>
        /// 加载背景精灵
        /// </summary>
        /// <param name="mapTypeEnum">地图枚举</param>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public static Sprite LoadBg(MapTypeEnum mapTypeEnum)
        {
            if(mapTypeEnum != MapTypeEnum.Other)
            {
                return Resources.Load<Sprite>(Config.MAP_TEXTURE_PATH + (int)mapTypeEnum);
            }
            Texture2D texture2D = new Texture2D(2, 2);
            texture2D.filterMode = FilterMode.Bilinear;
            byte[] fileData = LoadExternalMap();
            if(fileData != null)
            {
                if(texture2D.LoadImage(fileData))
                {
                    return Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                }
            }
            Debug.Log("未加载到自定义地图");
            return Sprite.Create(Texture2D.normalTexture, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
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
            return null;
        }

        public static SkeletonGraphic LoadSkeletonGraphic(string path, Transform parent = null)
        {
            SkeletonDataAsset asset = Load<SkeletonDataAsset>(path);
            return CreateSkeletonGraphic(asset, parent);
        }

        public static SkeletonGraphic LoadSkeletonGraphic(EntityModelType type, Transform parent = null)
        {
            if(ResLoaderTools.TryGetEntityCommonSpineDataAsset(type, out SkeletonDataAsset skeletonDataAsset))
            {
                return CreateSkeletonGraphic(skeletonDataAsset, parent);
            }
            Debug.LogError($"未加载到子弹动画{type.ToString()}");
            return default;
        }

        public static SkeletonGraphic LoadSkeletonGraphic(SkeletonDataAsset asset, Transform parent = null)
        {
            SkeletonGraphic skeletonGraphic = CreateSkeletonGraphic(asset, parent);
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
            if(ResLoaderTools.TryGetEntityBulletSpineDataAsset(entityModelType, out SkeletonDataAsset asset))
            {
                return CreateSkeletonGraphic(asset, parent);
            }
            Debug.LogError($"未加载到子弹动画{entityModelType.ToString()}");
            return default;
        }

        private static SkeletonGraphic CreateSkeletonGraphic(SkeletonDataAsset skeletonData, Transform parent = null)
        {
            GameObject go = Object.Instantiate(Load<GameObject>("Prefabs/SkeObject"), parent);
            SkeletonGraphic skeletonGraphic = go.GetComponent<SkeletonGraphic>();
            skeletonGraphic.skeletonDataAsset = skeletonData;
            skeletonGraphic.Initialize(true);
            skeletonGraphic.MatchRectTransformWithBounds();
            return skeletonGraphic;
        }

        public static T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}