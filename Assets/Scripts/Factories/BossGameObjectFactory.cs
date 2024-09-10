using System.Data;
using QFramework;
using Runtime.Data;
using Spine.Unity;
using UnityEngine;

namespace Factories
{
    public class BossGameObjectFactory : Singleton<BossGameObjectFactory>,IFactory
    {
        private BossGameObjectFactory()
        {
            
        }
        public SkeletonGraphic Create(EntityModelType type, Transform parent = null)
        {
            if(ResLoaderTools.TryGetEntityCommonSpineDataAsset(type, out SkeletonDataAsset asset))
            {
                return SkeletonGraphicFactory.Instance.Create(asset, parent);
            }
            throw new DataException($"Can't find SkeletonGraphic asset for type {type}");
        }
    }
}