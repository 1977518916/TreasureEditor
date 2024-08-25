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
        if (stateMachineComponent.Entity is HeroEntity)
        {
            stateMachineComponent.Entity.GetSpecifyComponent<HeroDeadComponent>(ComponentType.DeadComponent).Dead();
        }

        if (stateMachineComponent.Entity is EnemyEntity)
        {
            stateMachineComponent.Entity.GetSpecifyComponent<EnemyDeadComponent>(ComponentType.DeadComponent).Dead();
        }
        AnimationComponent.ChangeAnima(StateType.Dead, false, () =>
        {
            EntitySystem.Instance.DeadEntityRelease(stateMachineComponent.Entity.EntityId);
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
