using System;
using Spine.Unity;
using UnityEngine;

public class HeroAnimationComponent : AnimationComponent
{
    private long entityId;
    
    public SkeletonGraphic SkeletonGraphic { get; set; }

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
        if (stateType == StateType.Hit)
        {
            SkeletonGraphic.gameObject.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0.5f);
        }
        else
        {
            SkeletonGraphic.AnimationState.SetAnimation(0, GetAnimaName(stateType), isLoop).Complete +=
                entry => action?.Invoke();
        }
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
