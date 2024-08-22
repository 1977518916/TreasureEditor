using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 英雄状态机组件
/// </summary>
public class HeroStateMachineComponent : StateMachineComponent
{
    private HeroEntity heroEntity;

    private AnimationComponent animationComponent;

    private IState currentState;

    public StateType CurrentState { get; set; }
    public StateType LastState { get; set; }
    public Dictionary<StateType, List<StateType>> StateConvertDic { get; set; }
    public Dictionary<StateType, IState> AllStateDic { get; set; }

    public void Init(Entity entity, IState initState, Dictionary<StateType, List<StateType>> stateConvertDic,
        Dictionary<StateType, IState> allStateDic)
    {
        heroEntity = (HeroEntity)entity;
        animationComponent = heroEntity.GetSpecifyComponent<AnimationComponent>(ComponentType.AnimationComponent);
        currentState = initState;
        CurrentState = initState.StateType;
        LastState = StateType.None;
        StateConvertDic = stateConvertDic;
        AllStateDic = allStateDic;
    }
    
    public void ChangeState(StateType changeState)
    {
        QuitState();
        EnterState(AllStateDic[changeState]);
    }

    public void TryChangeState(StateType changeState)
    {
        if(!StateConvertDic[changeState].Contains(changeState) || currentState.Priority() > AllStateDic[changeState].Priority())
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
    
    public void Tick(float time)
    {
        currentState.Tick();
    }

    public void Release()
    {

    }
}
