using System.Collections.Generic;
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
        var targetId = GetFrontRowHeroID();
        var root = Instantiate(battleManager.BossRootPrefab, battleManager.BossParent);
        var entity = root.AddComponent<BossEntity>();
        var bossAnima = AssetsLoadManager.LoadSkeletonGraphic(modelType, entity.transform);
        entity.Init();
        entity.InitBoss(data);
        AddEntity(entity.EntityId, entity);
        // 初始化敌人检测
        InitPointDetect(entity, root.GetComponent<RectTransform>(), EntityType.HeroEntity, 500f);
        // 初始化敌人状态机组件 和 动画组件
        InitEnemyState(entity, InitEnemyEntityAnimation(entity.EntityId, bossAnima, entity));
        // 初始化固定距离移动组件
        InitFixedDistanceComponent(entity, entity.GetComponent<RectTransform>(), data);
        // 初始化出生点组件
        InitBossPosition(entity);
    }

    private void InitBossPosition(Entity entity)
    {
        entity.AllComponentList.Add(new RandomPositionComponent());
        entity.GetSpecifyComponent<RandomPositionComponent>(ComponentType.RandomPositionComponent)
            .BossPosition(entity.GetSpecifyComponent<MoveComponent>(ComponentType.MoveComponent).EntityTransform);
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
            new Vector2(-45f, 0f)));
    }
}
