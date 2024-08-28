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
        animaTimer?.Cancel();
        animaTimer = null;
    }

    public SkeletonGraphic GetSkeletonGraphic()
    {
        return SkeletonGraphic;
    }

    public void ChangeAnima(StateType stateType, bool isLoop, Action action)
    {
        animaTimer?.Cancel();
        if (stateType == StateType.Hit) return;
        foreach (var animation in SkeletonGraphic.AnimationState.Data.SkeletonData.Animations.Items)
        {
            if (!animation.Name.ToLower().Equals(stateType.ToString().ToLower())) continue;
            SkeletonGraphic.AnimationState.SetAnimation(0, animation.Name, isLoop);
            animaTimer = Timer.Register(SkeletonGraphic.AnimationState.Data.SkeletonData.FindAnimation(animation.Name).Duration,
                () =>
                {
                    if (SkeletonGraphic != null) 
                    {
                        action?.Invoke();
                    }
                }, isLooped: isLoop);
        }
    }
    
    public bool HasAnimation(StateType stateType, Action<bool> action = null)
    {
        foreach (var animation in SkeletonGraphic.AnimationState.Data.SkeletonData.Animations.Items)
        {
            if (animation.Name.ToLower().Equals(stateType.ToString().ToLower()))
            {
                action?.Invoke(true);
                return true;
            }   
        }
        action?.Invoke(false);
        return false;
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
            case StateType.Skill_1:
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
