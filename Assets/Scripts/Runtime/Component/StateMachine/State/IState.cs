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
    /// 动画组件
    /// </summary>
    public AnimationComponent AnimationComponent { get; set; }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(AnimationComponent animationComponent);

    /// <summary>
    /// 进入状态
    /// </summary>
    public void Enter(StateMachineComponent stateMachine);
    
    /// <summary>
    /// 更新状态
    /// </summary>
    public void Tick();
    
    /// <summary>
    /// 退出状态
    /// </summary>
    public void Exit();

    /// <summary>
    /// 优先级,优先级大的状态不会被优先级小的状态所改变(同级可改变)
    /// </summary>
    /// <returns></returns>
    public int Priority();
}