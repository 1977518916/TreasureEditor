using System.Collections.Generic;

/// <summary>
/// 状态机组件  TODO:目前有个缺陷问题是,逻辑控制状态机转变,但是有可能会遇到状态冲突的问题 这个需要后续解决 主要是动画表现上的问题
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
    public Dictionary<StateType, List<StateType>> StateConvertDic { get; set; }
    
    /// <summary>
    /// 所有状态字典
    /// </summary>
    public Dictionary<StateType, IState> AllStateDic { get; set; }
    
    /// <summary>
    /// 实体
    /// </summary>
    public Entity Entity { get; set; }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="initState"> 初始状态 </param>
    /// <param name="stateConvertDic"> 状态转化字典 </param>
    /// <param name="allStateDic"> 所有状态 </param>
    public void Init(Entity entity, IState initState, Dictionary<StateType, List<StateType>> stateConvertDic, Dictionary<StateType, IState> allStateDic);

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
