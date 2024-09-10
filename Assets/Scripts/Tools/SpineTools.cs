using Spine.Unity;

/// <summary>
/// Spine相关工具
/// </summary>
public static class SpineTools
{
    /// <summary>
    /// 动画资源替换
    /// </summary>
    public static void SkeletonDataAssetReplace(SkeletonGraphic graphic, SkeletonDataAsset dataAsset)
    {
        graphic.skeletonDataAsset = dataAsset;
        graphic.Initialize(true);
    }
}