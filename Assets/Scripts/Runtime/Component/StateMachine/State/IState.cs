using Spine.Unity;

/// <summary>
/// 状态接口
/// </summary>
public interface IState
{
    /// <summary>
    /// 状态
    /// </summary>
    public StateType StateType { get; }
    
    /// <summary>
    /// 动画文件
    /// </summary>
    public SkeletonGraphic SkeletonGraphic { set; }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(SkeletonGraphic skeletonGraphic);

    /// <summary>
    /// 进入状态
    /// </summary>
    public void Enter();
    
    /// <summary>
    /// 更新状态
    /// </summary>
    public void Tick();
    
    /// <summary>
    /// 退出状态
    /// </summary>
    public void Exit();
}