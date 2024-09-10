using Spine.Unity;

/// <summary>
/// Spine相关工具
/// </summary>
public static class SpineTools
{
    /// <summary>
    /// 动画资源替换  只负责数据编辑界面调用使用 不可使用在战斗场景中
    /// </summary>
    public static void SkeletonDataAssetReplace(SkeletonGraphic graphic, SkeletonDataAsset dataAsset)
    {
        graphic.skeletonDataAsset = dataAsset;
        graphic.Initialize(true);
        graphic.AnimationState.SetAnimation(0, graphic.SkeletonData.Animations.Items[0].Name, true);
    }
    
    /// <summary>
    /// 动画资源替换  只负责数据编辑界面调用使用 不可使用在战斗场景中
    /// </summary>
    /// <param name="graphic"></param>
    /// <param name="dataAsset"></param>
    /// <param name="animName"> 想要播放的动画的名字 </param>
    public static void SkeletonDataAssetReplace(SkeletonGraphic graphic, SkeletonDataAsset dataAsset, string animName)
    {
        graphic.skeletonDataAsset = dataAsset;
        graphic.Initialize(true);
        graphic.AnimationState.SetAnimation(0, animName, true);
    }
}