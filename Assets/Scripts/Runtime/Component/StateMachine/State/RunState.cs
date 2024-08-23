using Spine.Unity;
using UnityEngine;

public class RunState : IState
{
    public StateType StateType => StateType.Run;
    public AnimationComponent AnimationComponent { get; set; }

    public void Init(AnimationComponent animationComponent)
    {
        AnimationComponent = animationComponent;
    }
    
    public void Enter(StateMachineComponent stateMachineComponent)
    {
        AnimationComponent.ChangeAnima(StateType.Run, true, () => {});
    }

    public void Tick()
    {

    }

    public void Exit()
    {

    }
    
    public int Priority()
    {
        return 2;
    }
}
