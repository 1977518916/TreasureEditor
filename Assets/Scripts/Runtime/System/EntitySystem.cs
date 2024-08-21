using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Component.Position;
using Runtime.Data;
using Runtime.Manager;
using Spine.Unity;
using Tao_Framework.Core.Event;
using UnityEngine;

public class EntitySystem : MonoBehaviour
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
            (component as BulletMoveComponent)?.Tick(time);
        }
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
        // 获取英雄动画对象
        var heroAnima = heroModel.GetComponent<SkeletonGraphic>();
        // 初始化实体动画组件和动画
        InitEntityAnima(heroAnima);
        // 初始化实体状态组件
        InitHeroEntityStatus(heroEntity, indexValue);
        // 初始化攻击组件
        InitHeroEntityAttack(heroEntity);
        // 初始化检测组件
        InitDetect(heroEntity, "Enemy", "UI");
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
    
    private void InitDetect(Entity entity, string targetTag, string layerName)
    {
        var detect = new HostileDetectComponent(targetTag, LayerMask.GetMask(layerName), DetectRangeType.Square, entity);
        entity.AllComponentList.Add(detect);
    }

    private void InitEntityPosition(EnemyEntity entity)
    {
        entity.AllComponentList.Add(new RandomPositionComponent());
        entity.GetSpecifyComponent<RandomPositionComponent>(ComponentType.RandomPositionComponent)
            .RandomizePosition(entity.transform as RectTransform);
    }

    private void GenerateEntity(LevelManager.EnemyBean enemyBean)
    {
        GameObject root = Instantiate(battleManager.HeroRootPrefab, battleManager.EnemyParent);
        EnemyEntity entity = root.AddComponent<EnemyEntity>();
        entity.Init();
        var model = AssetsLoadManager.LoadEnemy(enemyBean.EnemyType, root.transform);
        model.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        model.transform.Translate(0, -30, 0, Space.Self);
        InitEntityPosition(entity);
        allEntityDic.Add(entity.EntityId, entity);
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEvent(GameEvent.MakeEnemy);
    }
}