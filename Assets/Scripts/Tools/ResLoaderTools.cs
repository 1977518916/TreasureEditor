using QFramework;
using Runtime.Data;
using Runtime.Manager;
using Spine.Unity;

/// <summary>
/// 资源加载工具
/// </summary>
public static class ResLoaderTools
{
    /// <summary>
    /// 加载器
    /// </summary>
    private static readonly ResLoader Loader = ResLoader.Allocate();

    /// <summary>
    /// 尝试获取实体子弹动画文件
    /// </summary>
    /// <param name="modelType"></param>
    /// <param name="dataAsset"></param>
    /// <returns></returns>
    public static bool TryGetEntityBulletSpineDataAsset(EntityModelType modelType, out SkeletonDataAsset dataAsset)
    {
        dataAsset = default;
        if (DataManager.GetBulletPath(modelType).IsNullOrEmpty()) return false;
        dataAsset = Loader.LoadSync<SkeletonDataAsset>(DataManager.GetBulletPath(modelType));
        return true;
    }
    
    /// <summary>
    /// 尝试获取实体技能动画文件
    /// </summary>
    /// <param name="skillKey"></param>
    /// <param name="dataAsset"></param>
    /// <returns></returns>
    public static bool TryGetEntitySkillSpineDataAsset(string skillKey, out SkeletonDataAsset dataAsset)
    {
        dataAsset = default;
        if (DataManager.GetSkillPath(skillKey).IsNullOrEmpty()) return false;
        dataAsset = Loader.LoadSync<SkeletonDataAsset>(DataManager.GetSkillPath(skillKey));
        return true;
    }

    /// <summary>
    /// 尝试获取实体寻常动画文件
    /// </summary>
    /// <param name="modelType"></param>
    /// <param name="dataAsset"></param>
    /// <returns></returns>
    public static bool TryGetEntityCommonSpineDataAsset(EntityModelType modelType, out SkeletonDataAsset dataAsset)
    {
        dataAsset = default;
        if (DataManager.GetCommonSpinePath(modelType).IsNullOrEmpty()) return false;
        dataAsset = Loader.LoadSync<SkeletonDataAsset>(DataManager.GetCommonSpinePath(modelType));
        return true;
    }
}
