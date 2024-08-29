/// <summary>
/// 子弹特性组件
/// </summary>
public interface BulletAttribute : AttributeComponent
{
    /// <summary>
    /// 子弹特性
    /// </summary>
    public BulletAttributeType AttributeType { get; set; }
    
    /// <summary>
    /// 执行特效效果
    /// </summary>
    public void Execute();
}