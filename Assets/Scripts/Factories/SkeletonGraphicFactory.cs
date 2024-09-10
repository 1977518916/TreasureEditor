using QFramework;
using Spine.Unity;
using UnityEngine;

namespace Factories
{
    public class SkeletonGraphicFactory : Singleton<SkeletonGraphicFactory>, IFactory
    {
        private SkeletonGraphicFactory()
        {
            
        }
        public SkeletonGraphic Create(SkeletonDataAsset dataAsset, Transform parent = null, bool isLoop = false)
        {
            GameObject go = Object.Instantiate(ResLoaderTools.LoadPrefab("SkeObject"), parent);
            SkeletonGraphic skeletonGraphic = go.GetComponent<SkeletonGraphic>();
            SpineTools.SkeletonDataAssetReplace(skeletonGraphic, dataAsset);
            skeletonGraphic.MatchRectTransformWithBounds();
            skeletonGraphic.AnimationState.SetAnimation(0, skeletonGraphic.SkeletonData.Animations.Items[0].Name, isLoop);
            return skeletonGraphic;
        }
    }
}