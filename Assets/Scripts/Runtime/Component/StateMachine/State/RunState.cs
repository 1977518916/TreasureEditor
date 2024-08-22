using Spine.Unity;
using UnityEngine;

public class RunState : IState
{
    public StateType StateType => StateType.Run;
    public SkeletonGraphic SkeletonGraphic { get; set; }
    
    public void Init(SkeletonGraphic skeletonGraphic)
    {
        SkeletonGraphic = skeletonGraphic;
    }
    
    public void Enter()
    {
        SkeletonGraphic.AnimationState.SetAnimation(0, "Run", false).Complete += entry =>
        {
            Debug.Log($"Run动画结束了");
        };
    }

    public void Tick()
    {

    }

    public void Exit()
    {

    }
}
