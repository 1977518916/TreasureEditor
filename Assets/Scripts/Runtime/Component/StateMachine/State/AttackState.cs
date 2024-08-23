using Spine.Unity;
using UnityEngine;

public class AttackState : IState
{
    public StateType StateType => StateType.Attack;
    public AnimationComponent AnimationComponent { get; set; }
    
    public void Init(AnimationComponent animationComponent)
    {
        AnimationComponent = animationComponent;
    }
    
    public void Enter(StateMachineComponent stateMachineComponent)
    {
        AnimationComponent.ChangeAnima(StateType.Attack, false, () =>
        {
            stateMachineComponent.ChangeState(StateType.Idle);
        });
    }
    
    public void Tick()
    {
        
    }

    public void Exit()
    {
        
    }
    public int Priority()
    {
        return 8;
    }
}
