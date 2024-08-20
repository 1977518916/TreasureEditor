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
        skeletonGraphic.AnimationState.SetAnimation(0, "Idle", false).Complete += entry =>
        {
            Debug.Log($"Idle动画结束了");
        };
    }

    public void Enter()
    {
    }

    public void Tick()
    {
    }

    public void Exit()
    {
    }
}
