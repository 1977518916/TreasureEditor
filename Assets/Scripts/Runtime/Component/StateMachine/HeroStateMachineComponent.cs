using System.Collections.Generic;

/// <summary>
/// 英雄状态机组件
/// </summary>
public class HeroStateMachineComponent : StateMachineComponent
{
    public StateType CurrentState { get; set; }
    
    public StateType LastState { get; set; }
    
    public List<StateConvert> StateConvertList { get; set; }
    
    public void Init(StateType initState, List<StateConvert> stateConverts)
    {
        
    }

    public void ChangeState(StateType changeState)
    {
        
    }

    public void TryChangeState(StateType changeState)
    {
        
    }

    public void Tick(float time)
    {
        
    }
}
