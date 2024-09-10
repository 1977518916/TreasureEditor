using System;
using System.Data;
using QFramework;
using Runtime.Data;
using Spine.Unity;
using UnityEngine;

namespace Factories
{
    public class HeroGameObjectFactory : Singleton<HeroGameObjectFactory>, IFactory
    {
        private HeroGameObjectFactory()
        {
            
        }
        public GameObject Create(EntityModelType heroTypeEnum, Transform parent)
        {
            if(ResLoaderTools.TryGetEntityCommonSpineDataAsset(heroTypeEnum, out var commonSpineDataAsset))
            {
                SkeletonGraphic skeletonAnimation = SkeletonGraphicFactory.Instance.Create(commonSpineDataAsset, parent);
                skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
                skeletonAnimation.name = heroTypeEnum.ToString();
                return skeletonAnimation.gameObject;
            }
            throw new DataException($"cannot find asset of{heroTypeEnum}");
        }
    }
}