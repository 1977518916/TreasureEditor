using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 英雄状态机组件
/// </summary>
public class HeroStateMachineComponent : StateMachineComponent
{
    public IState CurrentState { get; set; }
    
    public IState LastState { get; set; }
    
    public List<StateConvert> StateConvertList { get; set; }

    private HeroEntity heroEntity;

    private AnimationComponent animationComponent;
    
    public void Init(Entity entity, IState initState, List<StateConvert> stateConverts)
    {
        heroEntity = (HeroEntity)entity;
        animationComponent = heroEntity.GetSpecifyComponent<AnimationComponent>(ComponentType.AnimationComponent);
        CurrentState = initState;
        StateConvertList = stateConverts;
        CurrentState.Init(animationComponent);
    }
    
    public void ChangeState(IState changeState)
    {
        LastState = CurrentState;
        CurrentState = changeState;
        CurrentState.Init(animationComponent);
        CurrentState.Enter();
    }
    
    public void TryChangeState(IState changeState)
    {
        foreach (var stateConvert in from stateConvert in StateConvertList
                 where stateConvert.CurrentState == CurrentState.StateType
                 from state in stateConvert.ChangeState.Where(state => state.StateType == changeState.StateType)
                 select stateConvert)
        {
            ChangeState(changeState);
        }
    }
    
    public void Tick(float time)
    {
        CurrentState.Tick();
    }

    public void Release()
    {
        
    }
}
