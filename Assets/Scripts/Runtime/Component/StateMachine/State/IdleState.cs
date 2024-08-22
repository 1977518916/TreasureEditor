using Spine.Unity;
using UnityEngine;

/// <summary>
/// 待机状态
/// </summary>
public class IdleState : IState
{
    public StateType StateType => StateType.Idle;
    public SkeletonGraphic SkeletonGraphic { get; set; }

    public void Init(SkeletonGraphic skeletonGraphic)
    {
        SkeletonGraphic = skeletonGraphic;
    }

    public void Enter()
    {
        SkeletonGraphic.AnimationState.SetAnimation(0, "Idle", false).Complete += entry =>
        {
            Debug.Log($"Idle动画结束了");
        };
    }

    public void Tick()
    {
    }

    public void Exit()
    {
    }
}
