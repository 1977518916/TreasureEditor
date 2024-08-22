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
            Debug.Log($"攻击动画结束");
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
