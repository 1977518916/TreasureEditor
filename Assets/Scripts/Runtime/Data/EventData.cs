using Tao_Framework.Core.Event;

/// <summary>
/// 事件数据
/// </summary>
public struct EventData
{
    public GameEvent GameEvent;
    public IEventInfo EventInfo;
}

/// <summary>
/// UI事件数据
/// </summary>
public struct UIEventData
{
    public UIEvent UIEvent;
    public IEventInfo EventInfo;
}
