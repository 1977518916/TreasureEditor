using System;
using Runtime.Data;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Runtime.Manager
{
    public static class AssetsLoadManager
    {
        public static GameObject LoadHero(HeroTypeEnum heroTypeEnum, Transform parent)
        {
            return LoadSkeletonOfEnum(heroTypeEnum, parent).GameObject();
        }

        public static GameObject LoadEnemy(EnemyTypeEnum enemyTypeEnum, Transform parent)
        {
            return LoadSkeletonOfEnum(enemyTypeEnum, parent).GameObject();
        }

        public static Sprite LoadBg(MapTypeEnum mapTypeEnum)
        {
            return Resources.Load<Sprite>(DataManager.MapTexturePath + ((int)mapTypeEnum + 1));
        }

        public static SkeletonGraphic LoadSkeletonGraphic(string path, Transform parent)
        {
            SkeletonDataAsset asset = Load<SkeletonDataAsset>(path);
            SkeletonGraphic skeletonGraphic = SkeletonGraphic.NewSkeletonGraphicGameObject(asset, parent, Graphic.defaultGraphicMaterial);
            skeletonGraphic.AnimationState.SetAnimation(0, "Idle", true);
            return skeletonGraphic;
        }

        private static SkeletonGraphic LoadSkeletonOfEnum(Enum @enum, Transform parent)
        {
            string path = $"Character/{@enum.ToString()}/{@enum.ToString()}_SkeletonData";
            SkeletonGraphic skeletonAnimation = LoadSkeletonGraphic(path, parent);
            skeletonAnimation.name = @enum.ToString();
            return skeletonAnimation;
        }

        public static T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}