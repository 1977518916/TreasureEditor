using System.Collections.Generic;

/// <summary>
/// 状态机组件
/// </summary>
public interface StateMachineComponent : IComponent
{
    /// <summary>
    /// 当前处于的状态
    /// </summary>
    public StateType CurrentState { get; set; }

    /// <summary>
    /// 上一次的状态
    /// </summary>
    public StateType LastState { get; set; }

    /// <summary>
    /// 状态转换表
    /// </summary>
    public List<StateConvert> StateConvertList { get; set; }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="initState"> 初始状态 </param>
    /// <param name="stateConverts"> 状态转换表 </param>
    public void Init(StateType initState, List<StateConvert> stateConverts);

    /// <summary>
    /// 强制改变状态  某些可能极端的情况下会使用 不需要查询状态转换表直接切换
    /// </summary>
    /// <param name="changeState"> 切换过去的状态 </param>
    public void ChangeState(StateType changeState);

    /// <summary>
    /// 尝试改变状态  一般情况下建议使用这个 因为这会根据状态转换表尝试切换
    /// </summary>
    /// <param name="changeState"> 尝试切换的状态 </param>
    public void TryChangeState(StateType changeState);
}
