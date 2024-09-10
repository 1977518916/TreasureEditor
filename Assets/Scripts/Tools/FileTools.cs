using System.IO;

/// <summary>
/// 文件工具
/// </summary>
public static class FileTools
{
    /// <summary>
    /// 文件工具初始化
    /// </summary>
    public static void Init()
    {
        DetectAndCreateExternalMapFolder();
        DetectAndCreateExternalBulletFolder();
    }

    
    /// <summary>
    /// 检测并创建外部地图文件夹
    /// </summary>
    private static void DetectAndCreateExternalMapFolder()
    {
        if (!IsExistExternalMapFolder())
        {
            Directory.CreateDirectory(Config.MapExternalPath);
        }
    }
    
    /// <summary>
    /// 检测并创建外部子弹文件夹
    /// </summary>
    private static void DetectAndCreateExternalBulletFolder()
    {
        if (!IsExistExternalBulletFolder())
        {
            Directory.CreateDirectory(Config.BulletExternalPath);
        }
    }

    /// <summary>
    /// 是否存在外部地图文件夹
    /// </summary>
    /// <returns></returns>
    private static bool IsExistExternalMapFolder()
    {
        return File.Exists(Config.MapExternalPath);
    }
    
    /// <summary>
    /// 是否存在外部子弹文件夹
    /// </summary>
    /// <returns></returns>
    private static bool IsExistExternalBulletFolder()
    {
        return File.Exists(Config.BulletExternalPath);
    }
}