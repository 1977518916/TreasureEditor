using System.Collections.Generic;

/// <summary>
/// 实体接口
/// </summary>
public interface Entity
{
    /// <summary>
    /// 实体ID
    /// </summary>
    public long EntityId { get; set; }

    /// <summary>
    /// 所有组件
    /// </summary>
    public List<IComponent> AllComponentList { get; set; }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init();

    /// <summary>
    /// 获取指定组件
    /// </summary>
    /// <returns></returns>
    public T GetSpecifyComponent<T>(ComponentType componentType) where T : IComponent;

    /// <summary>
    /// 检测是否是指定组件
    /// </summary>
    /// <param name="component"> 组件 </param>
    /// <param name="componentType"> 组件类型 </param>
    /// <returns></returns>
    public bool IsSpecifyComponent(IComponent component, ComponentType componentType);
}
