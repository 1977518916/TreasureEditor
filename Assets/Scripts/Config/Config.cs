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
    /// 地图素材路径
    /// </summary>
    public const string MAP_TEXTURE_PATH = "Texture/LongMap/Map_";

    /// <summary>
    /// 所有技能数据
    /// </summary>
    public const string ALL_SKILL_DATA_PATH = "AllSkillData";

    /// <summary>
    /// 地图素材外部文件路径
    /// </summary>
    public static string MapExternalPath = $"{System.Environment.CurrentDirectory}/Map";

    /// <summary>
    /// 子弹文件外部路径
    /// </summary>
    public static string BulletExternalPath = $"{System.Environment.CurrentDirectory}/Bullet";

    #endregion
}
