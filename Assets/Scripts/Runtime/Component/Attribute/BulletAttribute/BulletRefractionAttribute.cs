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

    public BulletRefractionAttribute(int count)
    {
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
        
    }
}
