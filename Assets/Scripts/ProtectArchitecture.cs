using QFramework;
using Runtime.Data;
/// <summary>
/// 项目Qf架构
/// </summary>
public class ProtectArchitecture : Architecture<ProtectArchitecture>
{
    protected override void Init()
    {
        RegisterModel(new EnemiesDataModel());
    }
}