using System.Collections.Generic;
using Runtime.Component.Position;
using Runtime.Manager;
using Spine.Unity;
using UnityEngine;

/// <summary>
/// 实体系统 敌人模块
/// </summary>
public partial class EntitySystem
{
    /// <summary>
    /// 创建敌人实体
    /// </summary>
    /// <param name="enemyBean"></param>
    private void GenerateEnemyEntity(LevelManager.EnemyBean enemyBean)
    {
        var targetId = GetFrontRowHeroID();
        GameObject root = Instantiate(BattleManager.HeroAndEnemyRootPrefab, BattleManager.EnemyParent);
        root.tag = "Enemy";
        var entity = CreateEntity<EnemyEntity>(EntityType.EnemyEntity, root);
        var model = AssetsLoadManager.LoadEnemy(enemyBean.EnemyType, root.transform);
        var anim = model.GetComponent<SkeletonGraphic>();
        var scale = new Vector3(0.3f, 0.3f, 1f);
        root.transform.localScale *= enemyBean.EnemyData.modelScale;
        model.transform.localScale = scale * enemyBean.EnemyData.modelScale;
        model.transform.Translate(0, -30, 0, Space.Self);
        // 初始化敌人位置
        InitEntityPosition(entity, entity.GetComponent<RectTransform>());
        // 初始化敌人移动
        RectTransform targetRect = null;
        if (targetId != -1)
        {
            targetRect = GetEntity(targetId).GetSpecifyComponent<MoveComponent>(ComponentType.MoveComponent)
                .EntityTransform;
        }

        InitEnemyEntityMove(entity, targetRect, root.GetComponent<RectTransform>(), enemyBean.EnemyData.speed);
        // 初始化敌人检测
        InitPointDetect(entity, root.GetComponent<RectTransform>(), EntityType.HeroEntity, 150f);
        // 初始化敌人状态机组件 和 动画组件
        InitEnemyState(entity, InitEnemyEntityAnimation(entity.EntityId, anim, entity));
        //初始化敌人攻击
        entity.AllComponentList.Add(new EnemyAttackComponent(3f, enemyBean.EnemyData.atk, entity,
            entity.GetSpecifyComponent<PointDetectComponent>(ComponentType.DetectComponent)));
        //初始化敌人状态
        entity.AllComponentList.Add(new EnemyStatusComponent(enemyBean.EnemyData.hp, entity));
    }
    
    /// <summary>
    /// 初始化敌人动画组件
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="skeletonGraphic"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    private AnimationComponent InitEnemyEntityAnimation(long entityId, SkeletonGraphic skeletonGraphic, Entity entity)
    {
        var anima = new EnemyAnimationComponent(entityId, skeletonGraphic);
        entity.AllComponentList.Add(anima);
        return anima;
    }
    
    /// <summary>
    /// 初始化敌人实体移动组件
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="target"></param>
    /// <param name="entityTransform"></param>
    /// <param name="moveSpeed"></param>
    private void InitEnemyEntityMove(Entity entity, RectTransform target, RectTransform entityTransform, float moveSpeed)
    {
        entity.AllComponentList.Add(new EnemyMoveComponent((EnemyEntity)entity, target, entityTransform, moveSpeed));
    }
    
    /// <summary>
    /// 初始化敌人随机位置组件
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="rectTransform"></param>
    private void InitEntityPosition(Entity entity, RectTransform rectTransform)
    {
        entity.AllComponentList.Add(new RandomPositionComponent());
        entity.GetSpecifyComponent<RandomPositionComponent>(ComponentType.RandomPositionComponent)
            .RandomizePosition(rectTransform);
    }
    
    /// <summary>
    /// 初始化敌人状态
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="animationComponent"></param>
    private void InitEnemyState(Entity entity, AnimationComponent animationComponent)
    {
        var stateMachine = new EnemyStateMachineComponent();
        var attackState = new AttackState();
        var hitState = new HitState();
        var deadState = new DeadState();
        var moveState = new RunState();
        var idleState = new IdleState();
        moveState.Init(animationComponent);
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
                StateType.Run, moveState
            },
            {
                StateType.Attack, attackState
            },
            {
                StateType.Hit, hitState
            },
            {
                StateType.Dead, deadState
            }
        };
        stateMachine.Init(entity, moveState, stateConvertDic, allState);
        entity.AllComponentList.Add(stateMachine);
    }
}
