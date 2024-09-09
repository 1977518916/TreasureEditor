
/// <summary>
/// 配置类
/// </summary>
public static class Config
{
    #region Path

    /// <summary>
    /// 动画数据路径映射文件路径
    /// </summary>
    public const string SPINE_DATA_MAP_FILE_PATH = "Resources/DataFile/SpineData.es3";

    /// <summary>
    /// 动画路径中需要去除的前缀字符
    /// </summary>
    public const string SPINE_FILE_PREFIX_REPLACE = "Assets/Resources/";
    
    /// <summary>
    /// 动画路径中需要去除的后缀字符
    /// </summary>
    public const string SPINE_FILE_SUFFIX_REPLACE = ".asset";

    #endregion
}
