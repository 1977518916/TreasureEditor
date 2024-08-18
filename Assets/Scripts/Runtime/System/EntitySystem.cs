using System;
using System.Collections;
using System.Collections.Generic;
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
    public List<Entity> AllEntityList = new List<Entity>();
    
    private void Update()
    {
        currentTime += Time.time;
        if (currentTime >= UpdateTime)
        {
            EntityUpdate(UpdateTime);
        }
    }
    
    /// <summary>
    /// 实体更新函数
    /// </summary>
    private void EntityUpdate(float time)
    {
        foreach (var entity in AllEntityList)
        {
            foreach (var component in entity.AllComponentList)
            {
                (component as BulletMoveComponent)?.Move(time);
            }
        }
    }
}
