using System;
using System.Linq;
using Runtime.Data;

/// <summary>
/// 帮助工具  杂工具类
/// </summary>
public static class HelpTools
{
    /// <summary>
    /// 子弹对象是否是Spine动画
    /// </summary>
    /// <param name="modelType"></param>
    /// <returns></returns>
    public static bool BulletIsSpine(EntityModelType modelType)
    {
        return modelType < EntityModelType.XiaoBing_GongJian;
    }

    /// <summary>
    /// 子弹预制体路径   可以不断拓展来增加对应不同子弹预制体的路径
    /// </summary>
    /// <param name="modelType"></param>
    /// <returns></returns>
    public static string BulletPrefabPath(EntityModelType modelType)
    {
        return BulletIsArrow(modelType) ? BulletArrowPath() : "";
    }

    /// <summary>
    /// 箭 子弹路径
    /// </summary>
    /// <returns></returns>
    private static string BulletArrowPath()
    {
        return $"Prefabs/Jian";
    }

    /// <summary>
    /// 子弹是否是  箭   只需要一直拓展即可
    /// </summary>
    /// <param name="modelType"></param>
    /// <returns></returns>
    private static bool BulletIsArrow(EntityModelType modelType)
    {
        return modelType == EntityModelType.XiaoBing_GongJian;
    }

    /// <summary>
    /// 获取枚举值对应的名字
    /// </summary>
    /// <returns></returns>
    public static string GetEnumValueName<T>(object enumValue)
    {
        return Enum.GetName(typeof(T), enumValue);
    }

    /// <summary>
    /// 动画数据文件路径替换
    /// </summary>
    /// <param name="replace"></param>
    /// <param name="path"></param>
    /// <param name="replaceValue"></param>
    /// <returns></returns>
    public static string Replace(this string path, string[] replace, string replaceValue)
    {
        return replace.Aggregate(path, (current, s) => current.Replace(s, replaceValue));
    }
}