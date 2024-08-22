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
    
    public void Enter()
    {
        AnimationComponent.ChangeAnima(StateType.Attack, false, () => { Debug.Log($"攻击动画结束"); });
    }

    public void Tick()
    {
        
    }

    public void Exit()
    {
        
    }
}
