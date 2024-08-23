using Spine.Unity;
using UnityEngine;

/// <summary>
/// 待机状态
/// </summary>
public class IdleState : IState
{
    public StateType StateType => StateType.Idle;
    public AnimationComponent AnimationComponent { get; set; }
    
    public void Init(AnimationComponent animationComponent)
    {
        AnimationComponent = animationComponent;
    }
    
    public void Enter(StateMachineComponent stateMachineComponent)
    {
        AnimationComponent.ChangeAnima(StateType.Idle, true, () => {});
    }
    
    public void Tick()
    {
        
    }

    public void Exit()
    {
        
    }
    public int Priority()
    {
        return 0;
    }
}
