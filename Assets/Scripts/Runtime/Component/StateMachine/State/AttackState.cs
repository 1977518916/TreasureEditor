using Spine.Unity;
using UnityEngine;

public class AttackState : IState
{
    public StateType StateType => StateType.Attack;
    public SkeletonGraphic SkeletonGraphic { get; set; }
    
    public void Init(SkeletonGraphic skeletonGraphic)
    {
        SkeletonGraphic = skeletonGraphic;
    }
    
    public void Enter()
    {
        SkeletonGraphic.AnimationState.SetAnimation(0, "Attack", false).Complete += entry =>
        {
            Debug.Log($"Attack动画结束了");
        };
    }

    public void Tick()
    {
        
    }

    public void Exit()
    {
        
    }
}
