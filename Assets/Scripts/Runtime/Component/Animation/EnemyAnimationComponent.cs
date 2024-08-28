using System;
using Spine.Unity;

public class EnemyAnimationComponent : AnimationComponent
{
    /// <summary>
    /// 实体ID
    /// </summary>
    private long entityId;

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
        foreach (var animation in SkeletonGraphic.AnimationState.Data.SkeletonData.Animations.Items)
        {
            if (animation.Name.ToLower().Equals(stateType.ToString().ToLower()))
            {
                SkeletonGraphic.AnimationState.SetAnimation(0, animation.Name, isLoop).Complete +=
                    entry => action?.Invoke();
            }   
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
