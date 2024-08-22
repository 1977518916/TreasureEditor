/// <summary>
/// 组件接口
/// </summary>
public interface IComponent
{
    public void Tick(float time);
    
    public void Release();
}
