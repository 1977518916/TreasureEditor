using UnityEngine;

/// <summary>
/// 子弹折射特性
/// </summary>
public class BulletRefractionAttribute : BulletAttribute
{
    public BulletAttributeType AttributeType => BulletAttributeType.Refraction;

    /// <summary>
    /// 折射次数
    /// </summary>
    private int refractionCount;

    private BulletEntity bulletEntity;

    public BulletRefractionAttribute(int count, BulletEntity entity)
    {
        bulletEntity = entity;
        refractionCount = count;
    }

    public void Tick(float time)
    {

    }

    public void Release()
    {

    }
    
    public void Execute()
    {
        refractionCount -= 1;
        bulletEntity.GetSpecifyComponent<BulletMoveComponent>(ComponentType.MoveComponent).MoveDirection =
            EntitySystem.Instance.GetEntity(EntitySystem.Instance.GetSurviveEnemyID())
                .GetSpecifyComponent<EnemyMoveComponent>(ComponentType.MoveComponent).EntityTransform.anchoredPosition;
        if (refractionCount == 0)
            EntitySystem.Instance.ReleaseEntity(bulletEntity.EntityId);
    }
}
