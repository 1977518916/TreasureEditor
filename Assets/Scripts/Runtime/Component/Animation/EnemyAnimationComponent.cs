using System;
using Spine.Unity;
using UnityTimer;

public class EnemyAnimationComponent : AnimationComponent
{
    /// <summary>
    /// 实体ID
    /// </summary>
    private long entityId;
    
    private Timer animaTimer;
    
    public SkeletonGraphic SkeletonGraphic { get; set; }

    public EnemyAnimationComponent(long entityId,SkeletonGraphic skeletonGraphic)
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
            case StateType.Hit:
                return "Hit";
            case StateType.Dead:
                return "Dead";
            case StateType.Appear:
                return "Appear";
            default://敌人动画目前仅包括上述这些
                throw new ArgumentOutOfRangeException(nameof(stateType), stateType, null);
        }
    }
}
