using System.Collections.Generic;
using Tools;
using UnityEngine;

public class BossEntity : MonoBehaviour, Entity
{

    public long EntityId { get; set; }
    public EntityType EntityType { get; private set; }
    public List<IComponent> AllComponentList { get; set; }

    public void Init()
    {
        EntityId = GlobalOnlyID.GetGlobalOnlyID();
        EntityType = EntityType.EnemyEntity;
        AllComponentList = new List<IComponent>();
    }
    public void Release()
    {
        throw new System.NotImplementedException();
    }
    public T GetSpecifyComponent<T>(ComponentType componentType) where T : IComponent
    {
        throw new System.NotImplementedException();
    }
    public bool IsSpecifyComponent(IComponent component, ComponentType componentType)
    {
        throw new System.NotImplementedException();
    }
}