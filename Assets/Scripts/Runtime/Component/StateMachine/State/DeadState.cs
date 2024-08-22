using Spine.Unity;
using UnityEngine;

public class DeadState : IState
{
    public StateType StateType => StateType.Dead;
    public SkeletonGraphic SkeletonGraphic { get; set; }

    public void Init(SkeletonGraphic skeletonGraphic)
    {
        SkeletonGraphic = skeletonGraphic;
    }
    
    public void Enter()
    {
        SkeletonGraphic.AnimationState.SetAnimation(0, "Dead", false).Complete += entry =>
        {
            Debug.Log($"Dead动画结束了");
        };
    }

    public void Tick()
    {

    }

    public void Exit()
    {

    }
}
