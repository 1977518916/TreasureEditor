using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    public long EntityId { get; set; }
    public List<IComponent> AllComponentList { get; set; }

    public void Init()
    {
        
    }
}
