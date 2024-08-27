using System.Collections.Generic;
using Runtime.Component.Attack;
using Runtime.Component.Position;
using Runtime.Data;
using Runtime.Manager;
using UnityEngine;

/// <summary>
/// 分类出来写Boss的逻辑
/// </summary>
public partial class EntitySystem
{
    /// <summary>
    /// 生成Boss实体
    /// </summary>
    /// <param name="modelType"></param>
    /// <param name="data"></param>
    private void GenerateBossEntity(EntityModelType modelType, BossData data)
    {
        var root = Instantiate(battleManager.BossRootPrefab, battleManager.BossParent);
        var entity = root.AddComponent<BossEntity>();
        var bossAnima = AssetsLoadManager.LoadSkeletonGraphic(modelType, entity.transform);
        bossAnima.transform.localScale *= data.modelScale;
        entity.Init();
        entity.InitBoss(data);
        AddEntity(entity.EntityId, entity);
        // 初始化出生点组件
        InitBossPosition(entity, entity.GetEntityTransform());
        // 初始化敌人状态机组件 和 动画组件
        InitBossStateMachine(entity, InitEnemyEntityAnimation(entity.EntityId, bossAnima, entity), modelType);
        // 初始化固定距离移动组件
        InitFixedDistanceComponent(entity, entity.GetComponent<RectTransform>(), data);
        // 初始化攻击
        InitBossAtk(entity, data);
        // 初始化Boss状态组件
        InitBossStatusComponent(entity, data);
        // 死亡
        InitEnemyDead(entity.EntityId, entity);
    }

    private void InitBossAtk(BossEntity entity, BossData data)
    {
        BossAttackComponent bossAttackComponent = new BossAttackComponent(2, data.Atk, entity, entity.transform as RectTransform, data.BulletType, data.EntityModelType);
        entity.AllComponentList.Add(bossAttackComponent);
    }
    
    private void InitBossPosition(Entity entity, RectTransform rectTransform)
    {
        entity.AllComponentList.Add(new RandomPositionComponent());
        entity.GetSpecifyComponent<RandomPositionComponent>(ComponentType.RandomPositionComponent)
            .BossPosition(rectTransform);
    }

    /// <summary>
    /// 初始化固定距离移动组件
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="rectTransform"></param>
    /// <param name="data"></param>
    private void InitFixedDistanceComponent(Entity entity, RectTransform rectTransform, BossData data)
    {
        entity.AllComponentList.Add(new FixedDistanceComponent(rectTransform, data.RunSpeed, new Vector2(-1f, 0f),
            new Vector2(93f, 0f)));
    }
    
    /// <summary>
    /// 初始化Boss状态组件
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="data"></param>
    private void InitBossStatusComponent(Entity entity, BossData data)
    {
        entity.AllComponentList.Add(new EnemyStatusComponent(data.Hp, entity));
    }

    /// <summary>
    /// 初始化Boss状态机
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="animationComponent"></param>
    /// <param name="entityModelType"></param>
    private void InitBossStateMachine(Entity entity, AnimationComponent animationComponent,
        EntityModelType entityModelType)

    {
        var stateMachine = new EnemyStateMachineComponent();
        var attackState = new AttackState();
        var hitState = new HitState();
        var deadState = new DeadState();
        var runState = new RunState();
        var idleState = new IdleState();
        runState.Init(animationComponent);
        attackState.Init(animationComponent);
        hitState.Init(animationComponent);
        deadState.Init(animationComponent);
        idleState.Init(animationComponent);
        var stateConvertDic = new Dictionary<StateType, List<StateType>>
        {
            {
                StateType.Idle, new List<StateType>
                {
                    StateType.Attack,
                    StateType.Hit,
                    StateType.Dead
                }
            },
            {
                StateType.Run, new List<StateType>
                {
                    StateType.Attack,
                    StateType.Hit,
                    StateType.Dead
                }
            },
            {
                StateType.Attack, new List<StateType>
                {
                    StateType.Idle,
                    StateType.Dead
                }
            },
            {
                StateType.Hit, new List<StateType>
                {
                    StateType.Idle,
                    StateType.Dead
                }
            }
        };
        var allState = new Dictionary<StateType, IState>
        {
            {
                StateType.Idle, idleState
            },
            {
                StateType.Run, runState
            },
            {
                StateType.Attack, attackState
            },
            {
                StateType.Hit, entityModelType == EntityModelType.DongZhuo ? idleState : hitState
            },
            {
                StateType.Dead, deadState
            }
        };
        stateMachine.Init(entity, runState, stateConvertDic, allState);
        entity.AllComponentList.Add(stateMachine);
    }
}