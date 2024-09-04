using System.Collections.Generic;
public class SkillEntity : Entity
{

    public long EntityId { get; set; }
    public EntityType EntityType { get; }
    public List<IComponent> AllComponentList { get; set; }
    public bool ReadyRelease { get; set; }
    public void Init()
    {
       
    }
    public void Release()
    {
        
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