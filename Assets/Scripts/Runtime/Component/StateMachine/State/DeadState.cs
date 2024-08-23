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
            if (stateMachineComponent.Entity is HeroEntity) 
            {
                stateMachineComponent.Entity.GetSpecifyComponent<HeroDeadComponent>(ComponentType.DeadComponent).Dead();
            }
            
            if (stateMachineComponent.Entity is EnemyEntity)
            {
                stateMachineComponent.Entity.GetSpecifyComponent<EnemyDeadComponent>(ComponentType.DeadComponent).Dead();
            }
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
