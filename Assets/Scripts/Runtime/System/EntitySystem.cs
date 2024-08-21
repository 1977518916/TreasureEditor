using System;
using System.Collections.Generic;
using Runtime.Data;
using Runtime.Manager;
using Spine;
using Spine.Unity;
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
    private readonly List<Entity> allEntityList = new List<Entity>();
    
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
    }

    private void Update()
    {
        currentTime += Time.time;
        if(currentTime >= UpdateTime)
        {
            EntityUpdate(UpdateTime);
        }
    }

    /// <summary>
    /// 实体更新函数
    /// </summary>
    private void EntityUpdate(float time)
    {
        foreach (var entity in allEntityList)
        {
            foreach (var component in entity.AllComponentList)
            {
                (component as BulletMoveComponent)?.Tick(time);
            }
        }
    }

    /// <summary>
    /// 生成英雄实体
    /// </summary>
    /// <param name="type"> 位置 </param>
    /// <param name="data"> 数据 </param>
    private void GenerateEntity(DataType.HeroPositionType type, HeroData data)
    {
        var indexValue = Convert.ToInt32(type);
        // 生成英雄实体
        var hero = Instantiate(battleManager.HeroRootPrefab, battleManager.HeroParent);
        var heroEntity = hero.AddComponent<HeroEntity>();
        var heroModel = AssetsLoadManager.LoadHero(data.heroTypeEnum, hero.GetComponent<RectTransform>());
        heroEntity.InitHero(data, heroModel, battleManager.GetFirePoint(indexValue));
        // 获取英雄动画对象
        var heroAnima = heroModel.GetComponent<SkeletonGraphic>();
        // 初始化实体动画组件和动画
        InitEntityAnima(heroAnima);
        // 初始化实体状态组件
        InitEntityStatus(heroEntity, indexValue);
        // 初始化攻击组件
        InitEntityAttack(heroEntity);
        // 初始化检测组件
        InitDetect(heroEntity);
        // 设置英雄实体模型到对应位置
        BattleManager.Instance.SetPrefabLocation(hero, indexValue);
    }
    
    private void InitEntityAnima(SkeletonGraphic skeletonGraphic)
    {
        skeletonGraphic.initialFlipX = true;
        skeletonGraphic.Initialize(true);
        skeletonGraphic.AnimationState.SetAnimation(0, "Idle", true);
    }
    
    private void InitEntityStatus(HeroEntity heroEntity, int value)
    {
        var heroStatusUI = BattleManager.Instance.GetHeroStatus(value);
        var status = new HeroStatusComponent(heroStatusUI.HpBg, heroStatusUI.CdBg, heroStatusUI.Hp, heroStatusUI.Cd, heroEntity);
        heroEntity.AllComponentList.Add(status);
    }
    
    private void InitEntityAttack(HeroEntity heroEntity)
    {
        var attack = new HeroAttackComponent(9, heroEntity, 3);
        heroEntity.AllComponentList.Add(attack);
    }
    
    private void InitDetect(HeroEntity heroEntity)
    {
        var detect = new HostileDetectComponent("Enemy", LayerMask.GetMask("UI"), DetectRangeType.Square, heroEntity);
        heroEntity.AllComponentList.Add(detect);
    }
    
    private void GenerateEntity(EnemyTypeEnum enemyTypeEnum)
    {
        EnemyEntity entity = new EnemyEntity();
        allEntityList.Add(entity);
    }
}