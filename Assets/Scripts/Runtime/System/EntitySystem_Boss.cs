using Runtime.Data;
using Runtime.Manager;

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
        var bossEntity = root.AddComponent<BossEntity>();
        var bossModel = AssetsLoadManager.LoadSkeletonGraphic(modelType, bossEntity.transform);
        bossEntity.Init();
        bossEntity.InitBoss(data);
        AddEntity(bossEntity.EntityId, bossEntity);
    }

    private void InitBossStatus()
    {
        
    }
}
