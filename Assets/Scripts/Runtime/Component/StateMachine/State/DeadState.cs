using Spine.Unity;
using Tao_Framework.Core.Event;
using UnityEngine;

public class DeadState : IState
{
    public StateType StateType => StateType.Dead;
    public AnimationComponent AnimationComponent { get; set; }

    public void Init(AnimationComponent animationComponent)
    {
        AnimationComponent = animationComponent;
    }
    
    public void Enter(StateMachineComponent stateMachineComponent)
    {
        AnimationComponent.ChangeAnima(StateType.Dead, false, () =>
        {
            // 死亡 播放死亡动画 关闭碰撞包围盒  死亡动画播放完以后直接删除实体
            EventMgr.Instance.TriggerEvent(GameEvent.EntityDead, stateMachineComponent.Entity.EntityType);
            EntitySystem.Instance.ReleaseEntity(stateMachineComponent.Entity.EntityId);
        });
    }

    public void Tick()
    {

    }
    
    public void Exit()
    {
        
    }
    public int Priority()
    {
        return 10;
    }
}
