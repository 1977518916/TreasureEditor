using System.Collections;
using System.Collections.Generic;
using Tao_Framework.Core.Event;
using UnityEngine;

/// <summary>
/// 敌人分为
/// </summary>
public class EnemyStatusComponent : StatusComponent
{
    /// <summary>
    /// 血量
    /// </summary>
    private int hpValue;
    
    /// <summary>
    /// 最大血量值
    /// </summary>
    private int maxHpValue;
    
    /// <summary>
    /// 自身entity
    /// </summary>
    private Entity entity;

    public EnemyStatusComponent(int maxHpValue, Entity entity)
    {
        this.maxHpValue = maxHpValue;
        hpValue = this.maxHpValue;
        this.entity = entity;
    }

    public void Tick(float time)
    {
        
    }

    public void Release()
    {
        
    }
    
    /// <summary>
    /// 每调用一次会扣除一次受击次数并且会 进入 受击状态
    /// </summary>
    public void Hit(int value)
    {
        BattleManager.Instance.GenerateHurtText(
            entity.GetSpecifyComponent<MoveComponent>(ComponentType.MoveComponent).EntityTransform.position, value, 1.5f);
        hpValue -= value;
        if (hpValue > 0)
        {
            entity.GetSpecifyComponent<EnemyStateMachineComponent>(ComponentType.StateMachineComponent)
                .TryChangeState(StateType.Hit);
            (entity as EnemyEntity)?.GetSpecifyComponent<EnemyMoveComponent>(ComponentType.MoveComponent)
                ?.StopMove(0.2f);
        }
        else
        {
            (entity as BossEntity)?.SetSurvive(false);
            (entity as EnemyEntity)?.SetSurvive(false);
            (entity as BossEntity)?.SetColliderEnabled(false);
            (entity as EnemyEntity)?.SetColliderEnabled(false);
            entity.GetSpecifyComponent<EnemyStateMachineComponent>(ComponentType.StateMachineComponent)
                .TryChangeState(StateType.Dead);
            (entity as EnemyEntity)?.GetSpecifyComponent<EnemyMoveComponent>(ComponentType.MoveComponent)
                ?.StopMove(1f);
            (entity as BossEntity)?.GetSpecifyComponent<FixedDistanceComponent>(ComponentType.MoveComponent)
                ?.StopMove(1f);
            // Debug.Log($"当前死亡对象ID：{entity.EntityId}");
        }
    }
}
