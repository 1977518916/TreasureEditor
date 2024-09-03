using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Runtime.Component.Position;
using Runtime.Component.Skill;
using Runtime.Data;
using Runtime.Manager;
using Sirenix.OdinInspector;
using Spine.Unity;
using Tao_Framework.Core.Event;
using Tao_Framework.Core.Singleton;
using UnityEngine;
using UnityTimer;

public partial class EntitySystem : MonoSingleton<EntitySystem>
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
    /// 计时器
    /// </summary>
    private Timer timer;

    /// <summary>
    /// 实体字典  ID对应对象
    /// </summary>
    [ShowInInspector]
    private readonly ConcurrentDictionary<long, Entity> allEntityDic = new ConcurrentDictionary<long, Entity>();

    /// <summary>
    /// 战斗管理
    /// </summary>
    private static BattleManager BattleManager => BattleManager.Instance;

    /// <summary>
    /// 实体类型和对应的实体类
    /// </summary>
    private readonly Dictionary<EntityType, Type> entityTypeDic = new Dictionary<EntityType, Type>
    {
        { EntityType.HeroEntity, typeof(HeroEntity) },
        { EntityType.EnemyEntity, typeof(EnemyEntity) },
        { EntityType.BulletEntity, typeof(BulletEntity) },
        { EntityType.BossEntity, typeof(BossEntity) },
        { EntityType.BoomEntity, typeof(BoomEntity) }
    };

    #region 生命周期

    private void Start()
    {
        EventMgr.Instance.RegisterEvent(GetHashCode(), GameEvent.EnterBattle, EnterBattle);
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
    /// 定义的销毁函数主要用来退出场景时使用
    /// </summary>
    /// <param name="action"></param>
    public void Destroy(Action action)
    {
        foreach (var entity in allEntityDic.Values)
        {
            entity.Release();
        }

        EventMgr.Instance.RemoveEvent(GetHashCode(), GameEvent.MakeEnemy);
        timer?.Cancel();
        timer = null;
        allEntityDic.Clear();
        action?.Invoke();
    }
    
    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEvent(GetHashCode(), GameEvent.EnterBattle);
    }
    
    #endregion

    #region Event

    /// <summary>
    /// 进入战斗场景
    /// </summary>
    private void EnterBattle()
    {
        foreach (var heroData in DataManager.HeroDatas)
        {
            GenerateEntity(heroData.Key, heroData.Value);
        }

        timer = Timer.Register(DataManager.LevelData.BossData.Time, () =>
        {
            if (DataManager.LevelData.BossData.EntityModelType == EntityModelType.Null) return;
            GenerateBossEntity(DataManager.LevelData.BossData.EntityModelType, DataManager.LevelData.BossData);
        });
        EventMgr.Instance.RegisterEvent<LevelManager.EnemyBean>(GetHashCode(), GameEvent.MakeEnemy,
            GenerateEnemyEntity);
    }

    #endregion
    
    /// <summary>
    /// 实体更新函数
    /// </summary>
    private void EntityUpdate(float time)
    {
        foreach (var entity in allEntityDic.Values)
        {
            if (entity.ReadyRelease)
            {
                allEntityDic.TryRemove(entity.EntityId, out var releaseEntity);
                releaseEntity.Release();
                continue;
            }

            foreach (var iComponent in entity.AllComponentList)
            {
                if (entity.ReadyRelease)
                {
                    allEntityDic.TryRemove(entity.EntityId, out var releaseEntity);
                    releaseEntity.Release();
                    break;
                }

                iComponent.Tick(time);
            }
        }
    }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T CreateEntity<T>(EntityType entityType, GameObject entity) where T : Entity, new()
    {
        Entity e;
        if (entityTypeDic.TryGetValue(entityType, out var value))
        {
            e = entity.AddComponent(value) as Entity;
            e!.Init();
            AddEntity(e.EntityId, e);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
        }

        return (T)e;
    }
    
    /// <summary>
    /// 添加实体进入管理
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="entity"></param>
    private void AddEntity(long entityId, Entity entity)
    {
        allEntityDic.GetOrAdd(entityId, entity);
    }
    
    /// <summary>
    /// 获取指定实体
    /// </summary>
    public Entity GetEntity(long entityId)
    {
        return allEntityDic.GetValueOrDefault(entityId);
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
    
    /// <summary>
    /// 获取所有英雄实体
    /// </summary>
    /// <returns></returns>
    public List<HeroEntity> GetAllHeroEntity()
    {
        return allEntityDic.Values.OfType<HeroEntity>().ToList();
    }

    /// <summary>
    ///  获取存活的敌人的ID
    /// </summary>
    /// <returns></returns>
    public long GetSurviveEnemyID()
    {
        foreach (var kEntity in allEntityDic)
        {
            if (kEntity.Value is not (EnemyEntity or BossEntity)) continue;
            if (kEntity.Value is BossEntity { IsSurvive: true } or EnemyEntity { IsSurvive: true })
            {
                return kEntity.Key;
            }

            return -1;
        }

        return -1;
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
            case EntityType.BulletEntity:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
        }

        return false;
    }
    
    /// <summary>
    /// 释放实体
    /// </summary>
    /// <param name="entityId"></param>
    public void ReleaseEntity(long entityId)
    {
        if (allEntityDic.TryGetValue(entityId, out var entity))
        {
            entity.ReadyRelease = true;
        }
    }
}