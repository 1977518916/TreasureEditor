using System;
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
    /// 所有实体
    /// </summary>
    private readonly Dictionary<long, Entity> allEntityDic = new Dictionary<long, Entity>();

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
        foreach (var component in allEntityDic.Values.SelectMany(entity => entity.AllComponentList))
        {
            component.Tick(time);
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
        if (data.heroTypeEnum == HeroTypeEnum.Null) return;
        var indexValue = Convert.ToInt32(type);
        // 生成英雄实体
        var hero = Instantiate(battleManager.HeroRootPrefab, battleManager.HeroParent);
        var heroEntity = hero.AddComponent<HeroEntity>();
        var heroModel = AssetsLoadManager.LoadHero(data.heroTypeEnum, hero.GetComponent<RectTransform>());
        heroEntity.Init();
        heroEntity.InitHero(data, heroModel, battleManager.GetFirePoint(indexValue));
        allEntityDic.Add(heroEntity.EntityId, heroEntity);
        // 获取英雄动画对象
        var heroAnima = heroModel.GetComponent<SkeletonGraphic>();
        // 初始化实体动画组件和动画
        InitEntityAnima(heroAnima);
        // 初始化实体状态组件
        InitHeroEntityStatus(heroEntity, indexValue);
        // 初始化攻击组件
        InitHeroEntityAttack(heroEntity);
        // 初始化检测组件
        InitDetect(heroEntity, "Enemy", "UI", hero.GetComponent<RectTransform>());
        // 初始化英雄移动组件
        InitHeroMove(heroEntity);
        // 设置英雄实体模型到对应位置
        BattleManager.Instance.SetPrefabLocation(hero, indexValue);
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
    
    private void InitHeroEntityAttack(HeroEntity heroEntity)
    {
        var attack = new HeroAttackComponent(9, heroEntity, 3);
        heroEntity.AllComponentList.Add(attack);
    }
    
    private void InitDetect(Entity entity, string targetTag, string layerName, RectTransform rectTransform)
    {
        var detect = new HostileDetectComponent(targetTag, LayerMask.GetMask(new[] { layerName }),
            DetectRangeType.Square, entity, rectTransform);
        entity.AllComponentList.Add(detect);
    }
    
    private void InitHeroMove(HeroEntity entity)
    {
        var move = new HeroMoveComponent(entity.GetComponent<RectTransform>());
        entity.AllComponentList.Add(move);
    }

    /// <summary>
    /// 初始化点检测
    /// </summary>
    /// <param name="enemyId"></param>
    /// <param name="entity"></param>
    /// <param name="entityTransform"></param>
    /// <param name="targetEntityType"></param>
    private void InitPointDetect(long enemyId, Entity entity, RectTransform entityTransform, EntityType targetEntityType)
    {
        var detect = new PointDetectComponent(enemyId, entity, entityTransform, targetEntityType);
        entity.AllComponentList.Add(detect);
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
        GameObject root = Instantiate(battleManager.HeroRootPrefab, battleManager.EnemyParent);
        EnemyEntity entity = root.AddComponent<EnemyEntity>();
        entity.Init();
        allEntityDic.Add(entity.EntityId, entity);
        var model = AssetsLoadManager.LoadEnemy(enemyBean.EnemyType, root.transform);
        model.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        model.transform.Translate(0, -30, 0, Space.Self);
        InitEntityPosition(entity);
        InitEnemyEntityMove(entity, GetEntity(targetId).GetSpecifyComponent<MoveComponent>(ComponentType.MoveComponent).EntityTransform,
            root.GetComponent<RectTransform>(), 100);
        InitPointDetect(targetId, entity, root.GetComponent<RectTransform>(), EntityType.HeroEntity);
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
                GetFrontRowHeroID();
                break;
            case EntityType.EnemyEntity:
                GetSurviveEnemyID();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
        }

        return default;
    }
    
    /// <summary>
    /// 获取存活英雄的ID
    /// </summary>
    /// <returns></returns>
    public long GetSurviveHeroID()
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

        return default;
    }
    
    /// <summary>
    /// 获取最前排英雄的ID 如果为-1 则为没有英雄存活
    /// </summary>
    /// <returns></returns>
    private long GetFrontRowHeroID()
    {
        var heroList = GetAllHeroEntity();
        var currentMaxIndex = 0;
        HeroEntity maxIndexHero = null;
        foreach (var entity in heroList)
        {
            if ((int)entity.GetHeroData().heroTypeEnum > currentMaxIndex)
            {
                currentMaxIndex = (int)entity.GetHeroData().heroTypeEnum;
                maxIndexHero = entity;
            }
        }

        return maxIndexHero != null ? maxIndexHero.EntityId : -1;
    }

    public List<HeroEntity> GetAllHeroEntity()
    {
        var heroList = new List<HeroEntity>();
        foreach (var eValue in allEntityDic.Values)
        {
            if (eValue is HeroEntity)
            {
                heroList.Add((HeroEntity)eValue);
            }
        }

        return heroList;
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

        return default;
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEvent(GameEvent.MakeEnemy);
    }
}