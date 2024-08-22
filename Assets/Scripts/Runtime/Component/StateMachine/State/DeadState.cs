using Spine.Unity;
using UnityEngine;

public class DeadState : IState
{
    public StateType StateType => StateType.Dead;
    public AnimationComponent AnimationComponent { get; set; }

    public void Init(AnimationComponent animationComponent)
    {
        AnimationComponent = animationComponent;
    }
    
    public void Enter(StateMachineComponent stateMachineComponent)
    {
        AnimationComponent.ChangeAnima(StateType.Dead, false, () =>
        {
            stateMachineComponent.Entity.GetSpecifyComponent<DeadComponent>(ComponentType.DeadComponent).Dead();
            Debug.Log($"死亡动画结束");
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
        return 10;
    }
}
