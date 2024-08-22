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
    
    public void Enter()
    {
        AnimationComponent.ChangeAnima(StateType.Dead, false, () => { Debug.Log($"死亡动画结束"); });
    }

    public void Tick()
    {

    }

    public void Exit()
    {

    }
}
