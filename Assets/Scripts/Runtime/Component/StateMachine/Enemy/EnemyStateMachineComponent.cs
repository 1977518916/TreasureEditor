using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 敌人状态机
/// </summary>
public class EnemyStateMachineComponent : StateMachineComponent
{
    public void Tick(float time)
    {
        throw new System.NotImplementedException();
    }

    public void Release()
    {
        throw new System.NotImplementedException();
    }

    public StateType CurrentState { get; set; }
    public StateType LastState { get; set; }
    public Dictionary<StateType, List<StateType>> StateConvertDic { get; set; }
    public Dictionary<StateType, IState> AllStateDic { get; set; }
    public void Init(Entity entity, IState initState, Dictionary<StateType, List<StateType>> stateConvertDic, Dictionary<StateType, IState> allStateDic)
    {
        throw new System.NotImplementedException();
    }

    public void ChangeState(StateType changeState)
    {
        throw new System.NotImplementedException();
    }

    public void TryChangeState(StateType changeState)
    {
        throw new System.NotImplementedException();
    }
}
