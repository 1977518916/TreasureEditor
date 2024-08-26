using Runtime.Data;
using Runtime.Component.Attack;
using Runtime.Component.Position;
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
        // 初始化出生点组件
        InitBossPosition(entity, entity.GetEntityTransform());
        // 初始化敌人状态机组件 和 动画组件
        InitEnemyState(entity, InitEnemyEntityAnimation(entity.EntityId, bossAnima, entity));
        // 初始化固定距离移动组件
        InitFixedDistanceComponent(entity, entity.GetComponent<RectTransform>(), data);
        //初始化攻击组件
        InitBossAtk(entity, data);
    }

    private void InitBossPosition(Entity entity, RectTransform rectTransform)
    {
        entity.AllComponentList.Add(new RandomPositionComponent());
        entity.GetSpecifyComponent<RandomPositionComponent>(ComponentType.RandomPositionComponent)
            .BossPosition(rectTransform);
    }

    private void InitBossAtk(BossEntity entity, BossData data)
    {
        BossAttackComponent bossAttackComponent = new BossAttackComponent(2, data.Atk, entity, entity.transform as RectTransform, data.BulletType);
        entity.AllComponentList.Add(bossAttackComponent);
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