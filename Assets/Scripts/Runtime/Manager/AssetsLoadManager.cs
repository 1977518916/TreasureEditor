using Runtime.Data;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Manager
{
    public static class AssetsLoadManager
    {
        public static GameObject LoadHero(HeroType heroType)
        {
            string path = $"Character/{heroType.ToString()}/{heroType.ToString()}_SkeletonData";
            SkeletonAnimation skeletonAnimation = LoadSkeletonAnimation(path);
            skeletonAnimation.name = heroType.ToString();
            return skeletonAnimation.GameObject();
        }

        public static SkeletonAnimation LoadSkeletonAnimation(string path)
        {
            SkeletonDataAsset asset = Resources.Load<SkeletonDataAsset>(path);
            SkeletonAnimation skeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(asset);
            return skeletonAnimation;
        }
    }
}