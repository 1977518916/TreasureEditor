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
        if (EntitySystem.Instance.GetSurviveEnemyID() != -1)
        {
            var target = EntitySystem.Instance.GetEntity(EntitySystem.Instance.GetSurviveEnemyID())
                .GetSpecifyComponent<EnemyMoveComponent>(ComponentType.MoveComponent).EntityTransform;
            bulletEntity.GetSpecifyComponent<BulletMoveComponent>(ComponentType.MoveComponent)
                .SetMoveDirection(target.position);
        }

        if (refractionCount == 0)
            EntitySystem.Instance.ReleaseEntity(bulletEntity.EntityId);
    }
}
