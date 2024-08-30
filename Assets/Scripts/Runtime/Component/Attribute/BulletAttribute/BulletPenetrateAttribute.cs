using UnityEngine;

/// <summary>
/// 子弹穿透特性组件
/// </summary>
public class BulletPenetrateAttribute : BulletAttribute
{
    /// <summary>
    /// 特性类型
    /// </summary>
    public BulletAttributeType AttributeType => BulletAttributeType.Penetrate;

    /// <summary>
    /// 子弹实体
    /// </summary>
    private BulletEntity bulletEntity;

    /// <summary>
    /// 穿透次数
    /// </summary>
    private int penetrateCount;

    public BulletPenetrateAttribute(int count, BulletEntity entity)
    {
        bulletEntity = entity;
        penetrateCount = count;
    }

    public void Tick(float time)
    {

    }

    public void Release()
    {

    }
    
    public void Execute()
    {
        penetrateCount -= 1;
        if (penetrateCount == 0)
            EntitySystem.Instance.ReleaseEntity(bulletEntity.EntityId);
    }
}
