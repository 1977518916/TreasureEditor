using System.Collections.Generic;
using System.IO;
using QFramework;
using Runtime.Data;
using Runtime.Manager;
using Spine.Unity;
using UnityEngine;

/// <summary>
/// 资源加载工具
/// </summary>
public static class ResLoaderTools
{
    /// <summary>
    /// 加载器
    /// </summary>
    private static readonly ResLoader Loader = ResLoader.Allocate();

    #region 资源加载部份

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

    /// <summary>
    /// 尝试获取地图图片
    /// </summary>
    /// <param name="index"></param>
    /// <param name="sprite"></param>
    /// <returns></returns>
    public static bool TryGetMapSprite(int index, out Sprite sprite)
    {
        sprite = default;
        if (index is > Config.MapMaxIndex or < 0) return false;
        sprite = Loader.LoadSync<Sprite>($"Map_{index}");
        return true;
    }

    /// <summary>
    /// 获取所有外部地图
    /// </summary>
    /// <returns></returns>
    public static List<Sprite> GetAllExternalMap()
    {
        var spriteList = new List<Sprite>();
        var fileInfoArray = Directory.GetFiles(Config.MapExternalPath);
        foreach (var file in fileInfoArray)
        {
            if (!file.GetFileName().Contains(".png") && !file.GetFileName().Contains(".jpg")) continue;
            var bytes = File.ReadAllBytes($"{Config.MapExternalPath}/{file.GetFileName()}");
            var texture2D = new Texture2D(2, 2)
            {
                filterMode = FilterMode.Bilinear
            };
            if (!texture2D.LoadImage(bytes)) continue;
            var sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
            spriteList.Add(sprite);
        }

        return spriteList;
    }

    /// <summary>
    /// 获取所有技能数据
    /// </summary>
    /// <returns></returns>
    public static SkillStruct GetAllSkillData()
    {
        return Loader.LoadSync<SkillStruct>(Config.ALL_SKILL_DATA_PATH);
    }

    /// <summary>
    /// 加载预制体
    /// </summary>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    public static GameObject LoadPrefab(string prefabName)
    {
        return Loader.LoadSync<GameObject>(prefabName);
    }
    
    /// <summary>
    /// 加载图片资源
    /// </summary>
    /// <param name="spriteName"></param>
    /// <returns></returns>
    public static Sprite LoadSprite(string spriteName)
    {
        return Loader.LoadSync<Sprite>(spriteName);
    }
    
    
    
    #endregion
}
