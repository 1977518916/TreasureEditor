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
    
    public void Enter()
    {
        AnimationComponent.ChangeAnima(StateType.Hit, false, () => { Debug.Log($"受击动画结束"); });
    }
    
    public void Tick()
    {
        
    }

    public void Exit()
    {
        
    }
}
