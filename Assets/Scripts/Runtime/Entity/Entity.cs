/// <summary>
/// 实体接口
/// </summary>
public interface Entity
{
    /// <summary>
    /// 实体ID
    /// </summary>
    public long EntityId { get; set; }
    
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init();
}
