using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Runtime.Component.Position;
using Runtime.Data;
using Runtime.Manager;
using Spine.Unity;
using Tao_Framework.Core.Event;
using Tao_Framework.Core.Singleton;
using UnityEngine;

public class EntitySystem : MonoSingleton<EntitySystem>
{
    /// <summary>
    /// 更新帧率  60帧率更新
    /// </summary>
    private const float UpdateTime = 1 / 60f;

    /// <summary>
    /// 当前运行时间
    /// </summary>
    private float currentTime;

    /// <summary>
    /// 实体字典  ID对应对象
    /// </summary>
    private readonly ConcurrentDictionary<long, Entity> allEntityDic = new ConcurrentDictionary<long, Entity>();
    
    /// <summary>
    /// 战斗管理
    /// </summary>
    private BattleManager battleManager;
    
    private void Start()
    {
        battleManager = BattleManager.Instance;
        foreach (var heroData in DataManager.HeroDatas)
        {
            GenerateEntity(heroData.Key, heroData.Value);
        }
        
        EventMgr.Instance.RegisterEvent<LevelManager.EnemyBean>(GameEvent.MakeEnemy, GenerateEntity);
    }
    
    private void Update()
    {
        currentTime += Time.time;
        if (currentTime >= UpdateTime)
        {
            EntityUpdate(UpdateTime);
        }
    }

    /// <summary>
    /// 实体更新函数
    /// </summary>
    private void EntityUpdate(float time)
    {
        foreach (var entity in allEntityDic.Values)
        {
            foreach (var iComponent in entity.AllComponentList)
            {
                iComponent.Tick(time);
            }
        }
    }

    /// <summary>
    /// 获取指定实体
    /// </summary>
    public Entity GetEntity(long entityId)
    {
        return allEntityDic.GetValueOrDefault(entityId);
    }

    /// <summary>
    /// 生成英雄实体
    /// </summary>
    /// <param name="type"> 位置 </param>
    /// <param name="data"> 数据 </param>
    private void GenerateEntity(DataType.HeroPositionType type, HeroData data)
    {
        var indexValue = Convert.ToInt32(type);
        if (data.heroTypeEnum == HeroTypeEnum.Null)
        {
            InitNullHeroStatus(indexValue);
            return;
        }
        
        // 生成英雄实体
        var hero = Instantiate(battleManager.HeroAndEnemyRootPrefab, battleManager.HeroParent);
        hero.tag = "Hero";
        var heroEntity = hero.AddComponent<HeroEntity>();
        var heroModel = AssetsLoadManager.LoadHero(data.heroTypeEnum, hero.GetComponent<RectTransform>());
        heroEntity.Init();
        heroEntity.InitHero(data, heroModel, battleManager.GetFirePoint(indexValue), indexValue);
        AddEntity(heroEntity.EntityId, heroEntity);
        // 获取英雄动画对象
        var heroAnima = heroModel.GetComponent<SkeletonGraphic>();
        // 初始化实体动画组件和动画
        InitEntityAnima(heroAnima);
        // 初始化实体状态组件
        InitHeroEntityStatus(heroEntity, indexValue);
        // 初始化检测组件
        InitPointDetect(heroEntity, hero.GetComponent<RectTransform>(), EntityType.EnemyEntity, 1500f);
        // 初始化攻击组件
        InitHeroEntityAttack(heroEntity,
            heroEntity.GetSpecifyComponent<PointDetectComponent>(ComponentType.DetectComponent));
        // 初始化英雄移动组件
        InitHeroMove(heroEntity);
        // 初始化英雄状态机组件 和 动画组件
        InitHeroStateMachine(heroEntity, InitHeroEntityAnimation(heroEntity.EntityId, heroAnima, heroEntity));
        // 初始化死亡组件
        InitHeroDead(heroEntity.EntityId, heroEntity);
        // 设置英雄实体模型到对应位置
        BattleManager.Instance.SetPrefabLocation(hero, indexValue);
    }

    private void InitNullHeroStatus(int value)
    {
        var heroStatusUI = BattleManager.Instance.GetHeroStatus(value);
        heroStatusUI.HpBg.SetActive(false);
        heroStatusUI.CdBg.SetActive(false);
    }

    private void InitEntityAnima(SkeletonGraphic skeletonGraphic)
    {
        skeletonGraphic.initialFlipX = true;
        skeletonGraphic.Initialize(true);
        skeletonGraphic.AnimationState.SetAnimation(0, "Idle", true);
    }

    private void InitHeroEntityStatus(HeroEntity heroEntity, int value)
    {
        var heroStatusUI = BattleManager.Instance.GetHeroStatus(value);
        var status = new HeroStatusComponent(heroStatusUI.HpBg, heroStatusUI.CdBg, heroStatusUI.Hp, heroStatusUI.Cd,
            heroEntity);
        heroEntity.AllComponentList.Add(status);
    }

    private AnimationComponent InitHeroEntityAnimation(long entityId, SkeletonGraphic skeletonGraphic,
        HeroEntity heroEntity)
    {
        var anima = new HeroAnimationComponent(entityId, skeletonGraphic);
        heroEntity.AllComponentList.Add(anima);
        return anima;
    }
    
    private void InitHeroDead(long entityId, HeroEntity entity)
    {
        var dead = new HeroDeadComponent(entityId);
        entity.AllComponentList.Add(dead);
    }
    
    private void InitEnemyDead(long entityId, EnemyEntity entity)
    {
        var dead = new EnemyDeadComponent(entityId);
        entity.AllComponentList.Add(dead);
    }

    private void InitHeroEntityAttack(HeroEntity heroEntity, PointDetectComponent pointDetectComponent)
    {
        var attack = new HeroAttackComponent(heroEntity.GetHeroData().bulletAmount, 2.5f, 3, heroEntity, pointDetectComponent);
        heroEntity.AllComponentList.Add(attack);
    }

    /// <summary>
    /// 初始化点检测
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="entityTransform"></param>
    /// <param name="targetEntityType"></param>
    /// <param name="distance"></param>
    private void InitPointDetect(Entity entity, RectTransform entityTransform, EntityType targetEntityType,
        float distance)
    {
        var detect = new PointDetectComponent(entity, entityTransform, targetEntityType, distance);
        entity.AllComponentList.Add(detect);
    }

    private void InitHeroMove(HeroEntity entity)
    {
        var move = new HeroMoveComponent(entity.GetComponent<RectTransform>());
        entity.AllComponentList.Add(move);
    }

    /// <summary>
    /// 初始化英雄状态机
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="animationComponent"></param>
    private void InitHeroStateMachine(HeroEntity entity, AnimationComponent animationComponent)
    {
        var stateMachine = new HeroStateMachineComponent();
        var idleState = new IdleState();
        var attackState = new AttackState();
        var hitState = new HitState();
        var deadState = new DeadState();
        idleState.Init(animationComponent);
        attackState.Init(animationComponent);
        hitState.Init(animationComponent);
        deadState.Init(animationComponent);
        var stateConvertDic = new Dictionary<StateType, List<StateType>>
        {
            { StateType.Idle, new List<StateType> { StateType.Attack, StateType.Hit, StateType.Dead } },
            { StateType.Attack, new List<StateType> { StateType.Idle, StateType.Dead } },
            { StateType.Hit, new List<StateType> { StateType.Idle, StateType.Dead } }
        };
        var allState = new Dictionary<StateType, IState>
        {
            { StateType.Idle, idleState },
            { StateType.Attack, attackState },
            { StateType.Hit, hitState },
            { StateType.Dead, deadState }
        };
        stateMachine.Init(entity, idleState, stateConvertDic, allState);
        entity.AllComponentList.Add(stateMachine);
    }

    private void InitEnemyState(EnemyEntity entity, AnimationComponent animationComponent)
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
            { StateType.Idle, new List<StateType> { StateType.Attack, StateType.Hit, StateType.Dead } },
            { StateType.Run, new List<StateType> { StateType.Attack, StateType.Hit, StateType.Dead } },
            { StateType.Attack, new List<StateType> { StateType.Idle, StateType.Dead } },
            { StateType.Hit, new List<StateType> { StateType.Idle, StateType.Dead } }
        };
        var allState = new Dictionary<StateType, IState>
        {
            { StateType.Idle, idleState },
            { StateType.Run, moveState },
            { StateType.Attack, attackState },
            { StateType.Hit, hitState },
            { StateType.Dead, deadState }
        };
        stateMachine.Init(entity, moveState, stateConvertDic, allState);
        entity.AllComponentList.Add(stateMachine);
    }

    private AnimationComponent InitEnemyEntityAnimation(long entityId, SkeletonGraphic skeletonGraphic,
        EnemyEntity entity)
    {
        var anima = new EnemyAnimationComponent(entityId, skeletonGraphic);
        entity.AllComponentList.Add(anima);
        return anima;
    }

    private void InitEntityPosition(EnemyEntity entity)
    {
        entity.AllComponentList.Add(new RandomPositionComponent());
        entity.GetSpecifyComponent<RandomPositionComponent>(ComponentType.RandomPositionComponent)
            .RandomizePosition(entity.transform as RectTransform);
    }

    private void InitEnemyEntityMove(Entity entity, RectTransform target, RectTransform entityTransform,
        float moveSpeed)
    {
        entity.AllComponentList.Add(new EnemyMoveComponent((EnemyEntity)entity, target, entityTransform, moveSpeed));
    }

    private void GenerateEntity(LevelManager.EnemyBean enemyBean)
    {
        var targetId = GetFrontRowHeroID();
        GameObject root = Instantiate(battleManager.HeroAndEnemyRootPrefab, battleManager.EnemyParent);
        root.tag = "Enemy";
        EnemyEntity entity = root.AddComponent<EnemyEntity>();
        entity.Init();
        AddEntity(entity.EntityId, entity);
        var model = AssetsLoadManager.LoadEnemy(enemyBean.EnemyType, root.transform);
        var anim = model.GetComponent<SkeletonGraphic>();
        model.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        model.transform.Translate(0, -30, 0, Space.Self);
        // 初始化敌人位置
        InitEntityPosition(entity);
        // 初始化敌人移动
        InitEnemyEntityMove(entity,
            GetEntity(targetId).GetSpecifyComponent<MoveComponent>(ComponentType.MoveComponent).EntityTransform,
            root.GetComponent<RectTransform>(), 10f);
        // 初始化敌人检测
        InitPointDetect(entity, root.GetComponent<RectTransform>(), EntityType.HeroEntity, 150f);
        // 初始化敌人状态机组件 和 动画组件
        InitEnemyState(entity, InitEnemyEntityAnimation(entity.EntityId, anim, entity));
        // 初始化敌人死亡组件
        InitEnemyDead(entity.EntityId, entity);
        //初始化敌人攻击
        entity.AllComponentList.Add(new EnemyAttackComponent(3f, enemyBean.EnemyData.atk, entity,
            entity.GetSpecifyComponent<PointDetectComponent>(ComponentType.DetectComponent)));
        //初始化敌人状态
        entity.AllComponentList.Add(new EnemyStatusComponent(enemyBean.EnemyData.hp, entity));
    }
    
    /// <summary>
    /// 更换目标  如果返回的结果为-1证明目前需要找的对象没有
    /// </summary>
    /// <param name="entityType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public long ReplaceTarget(EntityType entityType)
    {
        switch (entityType)
        {
            case EntityType.HeroEntity:
                return GetFrontRowHeroID();
            case EntityType.EnemyEntity:
                return GetSurviveEnemyID();
            default:
                throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
        }
    }
    
    /// <summary>
    /// 获取存活英雄的ID
    /// </summary>
    /// <returns></returns>
    private long GetSurviveHeroID()
    {
        foreach (var kEntity in allEntityDic)
        {
            if (kEntity.Value is HeroEntity && kEntity.Value != null)
            {
                var hero = (HeroEntity)kEntity.Value;
                if (hero.GetIsSurvive())
                {
                    return hero.EntityId;
                }
            }
        }

        return -1;
    }
    
    /// <summary>
    /// 获取最前排英雄的ID 如果为-1 则为没有英雄存活
    /// </summary>
    /// <returns></returns>
    public long GetFrontRowHeroID()
    {
        if (GetSurviveHeroID() == -1) return -1;
        var heroList = GetAllHeroEntity();
        var currentMaxIndex = 0;
        HeroEntity maxIndexHero = null;
        foreach (var entity in heroList)
        {
            if (!entity.GetIsSurvive()) continue;
            if (entity.GetLocationIndex() < currentMaxIndex) continue;
            currentMaxIndex = entity.GetLocationIndex();
            maxIndexHero = entity;
        }

        return maxIndexHero != null ? maxIndexHero.EntityId : -1;
    }

    private List<HeroEntity> GetAllHeroEntity()
    {
        return allEntityDic.Values.OfType<HeroEntity>().ToList();
    }
    
    private List<EnemyEntity> GetAllEnemyEntity()
    {
        return allEntityDic.Values.OfType<EnemyEntity>().ToList();
    }

    /// <summary>
    /// 获取实体类型
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public EntityType GetEntityType(long entityId)
    {
        return allEntityDic.TryGetValue(entityId, out var entity) ? entity.EntityType : EntityType.None;
    }
    
    /// <summary>
    ///  获取存活的敌人的ID
    /// </summary>
    /// <returns></returns>
    public long GetSurviveEnemyID()
    {
        foreach (var kEntity in allEntityDic)
        {
            if (kEntity.Value is EnemyEntity)
            {
                return kEntity.Key;
            }
        }

        return -1;
    }
    
    /// <summary>
    /// 添加实体进入管理
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="entity"></param>
    public void AddEntity(long entityId, Entity entity)
    {
        allEntityDic.GetOrAdd(entityId, entity);
    }
    
    /// <summary>
    /// 获取目标类型是否有存活的敌人
    /// </summary>
    /// <param name="entityType"></param>
    /// <returns></returns>
    public bool GetTargetTypeSurviveEntity(EntityType entityType)
    {
        switch (entityType)
        {
            case EntityType.None:
                break;
            case EntityType.HeroEntity:
                return GetSurviveHeroID() != -1;
            case EntityType.EnemyEntity:
                return GetSurviveEnemyID() != -1;
            case EntityType.BulletEnemy:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
        }

        return false;
    }

    public void HeroDead(long entityId)
    {
        if (allEntityDic.TryGetValue(entityId, out var entity))
        {
            entity.Release();
        }
    }

    public void EnemyDead(long entityId)
    {
        if (allEntityDic.Remove(entityId, out var entity))
        {
            entity.Release();
        }
    }
    
    private void OnDestroy()
    {
        foreach (var entity in allEntityDic.Values) 
        {
            entity.Release();
        }
        
        allEntityDic.Clear();
        EventMgr.Instance.RemoveEvent(GameEvent.MakeEnemy);
    }
}