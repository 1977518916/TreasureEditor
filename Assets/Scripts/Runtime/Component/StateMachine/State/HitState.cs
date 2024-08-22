using Spine.Unity;
using UnityEngine;

public class HitState : IState
{
    public StateType StateType => StateType.Hit;
    public AnimationComponent AnimationComponent { get; set; }

    public void Init(AnimationComponent animationComponent)
    {
        AnimationComponent = animationComponent;
    }
    
    public void Enter(StateMachineComponent stateMachineComponent)
    {
        AnimationComponent.ChangeAnima(StateType.Hit, false, () =>
        {
            Debug.Log($"受击动画结束");
            stateMachineComponent.ChangeState(stateMachineComponent.LastState);
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
        return 7;
    }
}
