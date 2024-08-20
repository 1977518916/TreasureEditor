using System.Collections.Generic;

/// <summary>
/// 状态转换
/// </summary>
public struct StateConvert
{
    /// <summary>
    /// 当前状态
    /// </summary>
    public StateType CurrentState;

    /// <summary>
    /// 可以改变的状态 或者说 可以由当前状态切换过去的状态
    /// </summary>
    public List<StateType> ChangeState;
}