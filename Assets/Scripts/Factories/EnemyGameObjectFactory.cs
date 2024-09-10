using System.Data;
using QFramework;
using Runtime.Data;
using Spine.Unity;
using UnityEngine;

namespace Factories
{
    public class EnemyGameObjectFactory : Singleton<EnemyGameObjectFactory>,IFactory
    {
        private EnemyGameObjectFactory()
        {
            
        }
        public GameObject Create(EntityModelType enemyModelType, Transform parent)
        {
            if((int)enemyModelType < 500)
            {
                throw new DataException($"{enemyModelType} is not EnemyModel");
            }
            if(ResLoaderTools.TryGetEntityCommonSpineDataAsset(enemyModelType, out var commonSpineDataAsset))
            {
                SkeletonGraphic skeletonAnimation = SkeletonGraphicFactory.Instance.Create(commonSpineDataAsset, parent);
                skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
                skeletonAnimation.name = enemyModelType.ToString();
                return skeletonAnimation.gameObject;
            }
            throw new DataException($"cannot find asset of{enemyModelType}");
        } 
    }
}