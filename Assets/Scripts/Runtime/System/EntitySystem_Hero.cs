using System;
using System.Collections.Generic;
using Runtime.Component.Skill;
using Runtime.Data;
using Runtime.Manager;
using Spine.Unity;
using UnityEngine;

/// <summary>
/// 实体系统 英雄模块
/// </summary>
public partial class EntitySystem
{
    /// <summary>
    /// 生成英雄实体
    /// </summary>
    /// <param name="index"> 位置 </param>
    /// <param name="data"> 数据 </param>
    private void GenerateEntity(int index, HeroData data)
    {
        if (data.modelType == EntityModelType.Null)
        {
            InitNullHeroStatus(index);
            return;
        }

        // 生成英雄实体
        var hero = Instantiate(BattleManager.HeroAndEnemyRootPrefab, BattleManager.HeroParent);
        hero.tag = "Hero";
        var heroEntity = CreateEntity<HeroEntity>(EntityType.HeroEntity, hero);
        var heroModel = AssetsLoadManager.LoadHero(data.modelType, hero.GetComponent<RectTransform>());
        heroModel.GetComponent<RectTransform>().localScale *= data.modelScale;
        heroEntity.InitHero(data, BattleManager.GetFirePoint(index), index);
        // 获取英雄动画对象
        var heroAnima = heroModel.GetComponent<SkeletonGraphic>();
        // 初始化实体动画组件和动画
        InitEntityAnima(heroAnima);
        // 初始化实体状态组件
        InitHeroEntityStatus(heroEntity, index);
        // 初始化检测组件
        InitPointDetect(heroEntity, hero.GetComponent<RectTransform>(), EntityType.EnemyEntity, 1500f);
        // 初始化攻击组件
        InitHeroEntityAttack(heroEntity,
            heroEntity.GetSpecifyComponent<PointDetectComponent>(ComponentType.DetectComponent));
        // 初始化英雄移动组件
        InitHeroMove(heroEntity);
        // 初始化英雄状态机组件 和 动画组件
        InitHeroStateMachine(heroEntity, InitHeroEntityAnimation(heroEntity.EntityId, heroAnima, heroEntity));
        // 设置英雄实体模型到对应位置
        BattleManager.Instance.SetPrefabLocation(hero, index);
        // 初始化技能组件
        InitSkill(heroEntity);
    }

    /// <summary>
    /// 初始化技能组件
    /// </summary>
    /// <param name="positionType"></param>
    /// <param name="heroEntity"></param>
    private void InitSkill(HeroEntity heroEntity)
    {
        HeroSkillComponent skillComponent = new HeroSkillComponent(heroEntity);
        heroEntity.AllComponentList.Add(skillComponent);
    }

    /// <summary>
    /// 初始化空的英雄的状态栏
    /// </summary>
    /// <param name="value"></param>
    private void InitNullHeroStatus(int value)
    {
        var heroStatusUI = BattleManager.Instance.GetHeroStatus(value);
        heroStatusUI.HpBg.SetActive(false);
        heroStatusUI.CdBg.SetActive(false);
    }

    /// <summary>
    /// 初始化实体动画
    /// </summary>
    /// <param name="skeletonGraphic"></param>
    private void InitEntityAnima(SkeletonGraphic skeletonGraphic)
    {
        skeletonGraphic.initialFlipX = true;
        skeletonGraphic.Initialize(true);
        skeletonGraphic.AnimationState.SetAnimation(0, "Idle", true);
    }

    /// <summary>
    /// 初始化英雄实体状态组件
    /// </summary>
    /// <param name="heroEntity"></param>
    /// <param name="value"></param>
    private void InitHeroEntityStatus(HeroEntity heroEntity, int value)
    {
        var heroStatusUI = BattleManager.Instance.GetHeroStatus(value);
        var status = new HeroStatusComponent(heroStatusUI.HpBg, heroStatusUI.CdBg, heroStatusUI.Hp, heroStatusUI.Cd,
            heroEntity);
        heroEntity.AllComponentList.Add(status);
    }

    /// <summary>
    /// 初始化英雄实体动画组件
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="skeletonGraphic"></param>
    /// <param name="heroEntity"></param>
    /// <returns></returns>
    private AnimationComponent InitHeroEntityAnimation(long entityId, SkeletonGraphic skeletonGraphic,
        HeroEntity heroEntity)
    {
        var anima = new HeroAnimationComponent(entityId, skeletonGraphic);
        heroEntity.AllComponentList.Add(anima);
        return anima;
    }

    /// <summary>
    /// 初始化英雄实体攻击组件
    /// </summary>
    /// <param name="heroEntity"></param>
    /// <param name="pointDetectComponent"></param>
    private void InitHeroEntityAttack(HeroEntity heroEntity, PointDetectComponent pointDetectComponent)
    {
        HeroData data = heroEntity.GetHeroData();
        var attack = new HeroAttackComponent(data.bulletAmount, data.shooterAmount, data.atkInterval, 3, heroEntity,
            pointDetectComponent);
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

    /// <summary>
    /// 初始化英雄移动组件
    /// </summary>
    /// <param name="entity"></param>
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
        var skill1State = new Skill1State();
        var skill2State = new Skill2State();
        idleState.Init(animationComponent);
        attackState.Init(animationComponent);
        hitState.Init(animationComponent);
        deadState.Init(animationComponent);
        skill1State.Init(animationComponent);
        skill2State.Init(animationComponent);
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
            },
            {
                StateType.Skill_1, new List<StateType>()
                {
                    StateType.Idle,
                    StateType.Dead,
                    StateType.Attack
                }
            },
            {
                StateType.Skill_2, new List<StateType>()
                {
                    StateType.Idle,
                    StateType.Dead,
                    StateType.Attack
                }
            },
        };
        var allState = new Dictionary<StateType, IState>
        {
            {
                StateType.Idle, idleState
            },
            {
                StateType.Attack, attackState
            },
            {
                StateType.Hit, hitState
            },
            {
                StateType.Dead, deadState
            },
            {
                StateType.Skill_1, skill1State
            },
            {
                StateType.Skill_2, skill2State
            }
        };
        stateMachine.Init(entity, idleState, stateConvertDic, allState);
        entity.AllComponentList.Add(stateMachine);
    }
}
