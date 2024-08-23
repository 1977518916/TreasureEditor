using System;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;

public class HeroAnimationComponent : AnimationComponent
{
    private long entityId;
    
    public SkeletonGraphic SkeletonGraphic { get; set; }
    
    private AnimationState.TrackEntryDelegate  animaAction;
    
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
        SkeletonGraphic.AnimationState.Complete -= animaAction;
        animaAction = entry =>
        {
            action?.Invoke();
        };
        if (stateType != StateType.Hit)
        {
            SkeletonGraphic.AnimationState.SetAnimation(0, GetAnimaName(stateType), isLoop).Complete += animaAction;
        }
    }
    
    private string GetAnimaName(StateType stateType)
    {
        switch (stateType)
        {
            case StateType.Idle:
                Debug.Log($"{entityId}:进入Idle状态");
                return "Idle";
            case StateType.Run:
                return "Run";
            case StateType.Attack:
                Debug.Log($"{entityId}:进入Attack状态");
                return "Attack";
            case StateType.Skill:
                return "Skill";
            case StateType.Hit:
                Debug.Log($"{entityId}:进入Hit状态");
                return "Hit";
            case StateType.Dead:
                Debug.Log($"{entityId}:进入Dead状态");
                return "Dead";
            default:
                throw new ArgumentOutOfRangeException(nameof(stateType), stateType, null);
        }
    }
}
