/// <summary>
/// 状态类型
/// </summary>
public enum StateType
{
    None = -1,

    /// <summary>
    /// 待机
    /// </summary>
    Idle,

    /// <summary>
    /// 跑
    /// </summary>
    Run,

    /// <summary>
    /// 攻击
    /// </summary>
    Attack,

    /// <summary>
    /// 技能1引导
    /// </summary>
    Skill_1_Guide,
    
    /// <summary>
    /// 技能1释放
    /// </summary>
    Skill_1,
    
    /// <summary>
    /// 技能1结束
    /// </summary>
    Skill_1_End,
    
    /// <summary>
    /// 技能2
    /// </summary>
    Skill_2,

    /// <summary>
    /// 受击
    /// </summary>
    Hit,

    /// <summary>
    /// 死亡
    /// </summary>
    Dead,

    /// <summary>
    /// 出现 登场
    /// </summary>
    Appear
}