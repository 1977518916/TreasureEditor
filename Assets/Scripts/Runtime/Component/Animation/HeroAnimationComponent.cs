using System;
using Spine.Unity;
using UnityEngine;
using UnityTimer;
using AnimationState = Spine.AnimationState;

public class HeroAnimationComponent : AnimationComponent
{
    private long entityId;
    
    public SkeletonGraphic SkeletonGraphic { get; set; }

    private Timer animaTimer;
    
    public HeroAnimationComponent(long entityId, SkeletonGraphic skeletonGraphic)
    {
        this.entityId = entityId;
        SkeletonGraphic = skeletonGraphic;
    }

    public void Tick(float time)
    {
        
    }

    public void Release()
    {
        
    }

    public SkeletonGraphic GetSkeletonGraphic()
    {
        return SkeletonGraphic;
    }

    public void ChangeAnima(StateType stateType, bool isLoop, Action action)
    {
        animaTimer?.Cancel();
        if (stateType == StateType.Hit) return;
        SkeletonGraphic.AnimationState.SetAnimation(0, GetAnimaName(stateType), isLoop);
        animaTimer = Timer.Register(SkeletonGraphic.AnimationState.Data.SkeletonData.FindAnimation(GetAnimaName(stateType)).Duration,
            action, isLooped: isLoop);
    }

    private string GetAnimaName(StateType stateType)
    {
        switch (stateType)
        {
            case StateType.Idle:
                return "Idle";
            case StateType.Run:
                return "Run";
            case StateType.Attack:
                return "Attack";
            case StateType.Skill:
                return "Skill";
            case StateType.Hit:
                return "Hit";
            case StateType.Dead:
                return "Dead";
            default:
                throw new ArgumentOutOfRangeException(nameof(stateType), stateType, null);
        }
    }
}
