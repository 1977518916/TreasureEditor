using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEntity : MonoBehaviour, Entity
{
    public long EntityId { get; set; }
    public EntityType EntityType { get; }
    public List<IComponent> AllComponentList { get; set; }
    public bool ReadyRelease { get; set; }
    private string targetTag;
    private int hurtValue;
    public void Init()
    {
        
    }
    
    public void Init(string target, int hurt)
    {
        targetTag = target;
        hurtValue = hurt;
    }

    public void Release()
    {
        if (GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = false;
        }

        AllComponentList.Clear();
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            other.GetComponent<Entity>().GetSpecifyComponent<StatusComponent>(ComponentType.StatusComponent)
                .Hit(hurtValue);
            EntitySystem.Instance.ReleaseEntity(EntityId);
        }
    }

    public T GetSpecifyComponent<T>(ComponentType componentType) where T : IComponent
    {
        foreach (var iComponent in AllComponentList)
        {
            if (IsSpecifyComponent(iComponent, componentType))
            {
                return (T)iComponent;
            }
        }

        return default;
    }

    public bool IsSpecifyComponent(IComponent component, ComponentType componentType)
    {
        return componentType switch
        {
            ComponentType.AttackComponent => component is AttackComponent,
            ComponentType.MoveComponent => component is MoveComponent,
            ComponentType.RayComponent => false,
            ComponentType.StatusComponent => component is StatusComponent,
            ComponentType.DetectComponent => component is DetectComponent,
            ComponentType.AnimationComponent => component is AnimationComponent,
            ComponentType.StateMachineComponent => component is StateMachineComponent,
            ComponentType.DeadComponent => component is DeadComponent,
            ComponentType.Attribute => component is AttributeComponent,
            _ => throw new ArgumentOutOfRangeException(nameof(componentType), componentType, null)
        };
    }
}
