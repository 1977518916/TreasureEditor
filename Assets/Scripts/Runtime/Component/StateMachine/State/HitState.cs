using Spine.Unity;
using UnityEngine;

public class HitState : IState
{
    public StateType StateType => StateType.Hit;
    public SkeletonGraphic SkeletonGraphic { get; set; }
    
    public void Init(SkeletonGraphic skeletonGraphic)
    {
        SkeletonGraphic = skeletonGraphic;
    }
    
    public void Enter()
    {
        SkeletonGraphic.AnimationState.SetAnimation(0, "Hit", false).Complete += entry =>
        {
            Debug.Log($"Hit动画结束了");
        };
    }
    
    public void Tick()
    {
        
    }

    public void Exit()
    {
        
    }
}
