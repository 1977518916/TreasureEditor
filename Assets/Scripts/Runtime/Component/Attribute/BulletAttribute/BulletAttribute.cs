/// <summary>
/// 子弹特性组件
/// </summary>
public interface BulletAttribute : AttributeComponent
{
    /// <summary>
    /// 子弹特性
    /// </summary>
    public BulletAttributeType AttributeType { get; }

    /// <summary>
    /// 执行特性效果
    /// </summary>
    public void Execute();
}