using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 敌人状态机
/// </summary>
public class EnemyStateMachineComponent : StateMachineComponent
{
    public void Tick(float time)
    {
        currentState.Tick();
    }

    public void Release()  
    {
        
    }

    public StateType CurrentState { get; set; }
    public StateType LastState { get; set; }
    public Dictionary<StateType, List<StateType>> StateConvertDic { get; set; }
    public Dictionary<StateType, IState> AllStateDic { get; set; }
    public Entity Entity { get; set; }
    private IState currentState;
    public void Init(Entity entity, IState initState, Dictionary<StateType, List<StateType>> stateConvertDic, Dictionary<StateType, IState> allStateDic)
    {
        Entity = entity;
        currentState = initState;
        CurrentState = initState.StateType;
        LastState = StateType.None;
        StateConvertDic = stateConvertDic;
        AllStateDic = allStateDic;
        EnterState(initState);
    }

    public void ChangeState(StateType changeState)
    {
        QuitState();
        EnterState(AllStateDic[changeState]);
    }

    public void TryChangeState(StateType changeState)
    {
        if(!StateConvertDic[CurrentState].Contains(changeState) || currentState.Priority() > AllStateDic[changeState].Priority())
        {
            return;
        }
        QuitState();
        EnterState(AllStateDic[changeState]);
    }
    
    private void EnterState(IState state)
    {
        currentState = state;
        state.Enter(this);
    }

    private void QuitState()
    {
        LastState = CurrentState;
        currentState.Exit();
    }
}
