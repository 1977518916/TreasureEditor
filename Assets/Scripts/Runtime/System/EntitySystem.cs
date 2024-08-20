using System.Collections.Generic;
using Runtime.Data;
using Runtime.Manager;
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

    private void Start()
    {

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
    /// 生成实体
    /// </summary>
    /// <param name="type"> 类型 </param>
    /// <param name="data"> 数据 </param>
    /// <param name="index"> 位置 </param>
    private void GenerateEntity(DataType.HeroPositionType type, HeroData data, int index)
    {

    }

    private void GenerateEntity(EnemyTypeEnum enemyTypeEnum)
    {
        EnemyEntity entity = new EnemyEntity();
        allEntityList.Add(entity);
    }
}