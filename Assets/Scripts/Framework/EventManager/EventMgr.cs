using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Tao_Framework.Core.Singleton;
using UnityTimer;

namespace Tao_Framework.Core.Event
{
    public class EventMgr : MonoSingleton<EventMgr>
    {
        /// <summary>
        /// 游戏事件 无参 字典
        /// </summary>
        private ConcurrentDictionary<long, List<EventData>> _gameEventDic = new ConcurrentDictionary<long, List<EventData>>();

        /// <summary>
        /// UI事件字典
        /// </summary>
        private ConcurrentDictionary<long, List<UIEventData>> _uiEventDic = new ConcurrentDictionary<long, List<UIEventData>>();

        /// <summary>
        /// 注册事件 游戏事件 无参
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public void RegisterEvent(long entityID, GameEvent key, Action action)
        {
            if (_gameEventDic.TryGetValue(entityID, out var value))
            {
                value.Add(new EventData
                {
                    GameEvent = key,
                    EventInfo = new EventInfo(action)
                });
            }
            else
            {
                _gameEventDic.GetOrAdd(entityID, new List<EventData>
                {
                    new()
                    {
                        GameEvent = key,
                        EventInfo = new EventInfo(action)
                    }
                });
            }
        }

        /// <summary>
        /// 注册事件 UI事件 无参
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public void RegisterEvent(long entityID, UIEvent key, Action action)
        {
            if (_uiEventDic.TryGetValue(entityID, out var value))
            {
                value.Add(new UIEventData()
                {
                    UIEvent = key,
                    EventInfo = new EventInfo(action)
                });
            }
            else
            {
                _uiEventDic.GetOrAdd(entityID, new List<UIEventData>
                {
                    new()
                    {
                        UIEvent = key,
                        EventInfo = new EventInfo(action)
                    }
                });
            }
        }

        /// <summary>
        /// 注册事件 游戏事件 带参数的
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public void RegisterEvent<T>(long entityID, GameEvent key, Action<T> action)
        {
            if (_gameEventDic.TryGetValue(entityID, out var value))
            {
                value.Add(new EventData
                {
                    GameEvent = key,
                    EventInfo = new EventInfo<T>(action)
                });
            }
            else
            {
                _gameEventDic.GetOrAdd(entityID, new List<EventData>
                {
                    new()
                    {
                        GameEvent = key,
                        EventInfo = new EventInfo<T>(action)
                    }
                });
            }
        }

        /// <summary>
        /// 注册事件 UI事件 带参数的
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public void RegisterEvent<T>(long entityID, UIEvent key, Action<T> action)
        {
            if (_uiEventDic.TryGetValue(entityID, out var value))
            {
                value.Add(new UIEventData()
                {
                    UIEvent = key,
                    EventInfo = new EventInfo<T>(action)
                });
            }
            else
            {
                _uiEventDic.GetOrAdd(entityID, new List<UIEventData>
                {
                    new()
                    {
                        UIEvent = key,
                        EventInfo = new EventInfo<T>(action)
                    }
                });
            }
        }

        /// <summary>
        /// 清除事件 但是不移除对应的key
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="key"></param>
        public void ClearEvent(long entityID, GameEvent key)
        {
            if (!_gameEventDic.TryGetValue(entityID, out var value)) return;
            foreach (var eventData in value.Where(eventData => eventData.GameEvent == key))
            {
                (eventData.EventInfo as EventInfo)?.Clear();
            }
        }

        /// <summary>
        /// 清除事件 但是不移除对应的key
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="key"></param>
        public void ClearEvent(long entityID, UIEvent key)
        {
            if (!_uiEventDic.TryGetValue(entityID, out var value)) return;
            foreach (var eventData in value.Where(eventData => eventData.UIEvent == key))
            {
                (eventData.EventInfo as EventInfo)?.Clear();
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="key"></param>
        public void RemoveEvent(long entityID, GameEvent key)
        {
            if (!_gameEventDic.TryGetValue(entityID, out var value)) return;
            foreach (var eventData in value.Where(eventData => eventData.GameEvent == key).ToList())
            {
                value.Remove(eventData);
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="key"></param>
        public void RemoveEvent(long entityID, UIEvent key)
        {
            if (!_uiEventDic.TryGetValue(entityID, out var value)) return;
            foreach (var eventData in value.Where(eventData => eventData.UIEvent == key).ToList())
            {
                value.Remove(eventData);
            }
        }
        
        /// <summary>
        /// 触发无参事件
        /// </summary>
        /// <param name="key"></param>
        public void TriggerEvent(GameEvent key)
        {
            foreach (var eventData in _gameEventDic.SelectMany(ev => ev.Value.Where(eventData => eventData.GameEvent == key)))
            {
                (eventData.EventInfo as EventInfo)?.Invoke();
            }
        }
        
        /// <summary>
        /// 延时无参事件触发
        /// </summary>
        /// <param name="key"></param>
        /// <param name="time"></param>
        public void TriggerEvent(GameEvent key, float time)
        {
            Timer.Register(time, () =>
            {
                foreach (var eventData in _gameEventDic.SelectMany(ev => ev.Value.Where(eventData => eventData.GameEvent == key)))
                {
                    (eventData.EventInfo as EventInfo)?.Invoke();
                }
            });
        }

        /// <summary>
        /// 延时有参事件触发
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        public void TriggerEvent<T>(GameEvent key, T value, float time)
        {
            foreach (var eventData in _gameEventDic.SelectMany(ev => ev.Value.Where(eventData => eventData.GameEvent == key)))
            {
                Timer.Register(time, () => (eventData.EventInfo as EventInfo<T>)?.Invoke(value));
            }
        }

        /// <summary>
        /// 触发无参事件
        /// </summary>
        /// <param name="key"></param>
        public void TriggerEvent(UIEvent key)
        {
            foreach (var eventData in _uiEventDic.SelectMany(ev => ev.Value.Where(eventData => eventData.UIEvent == key)))
            {
                (eventData.EventInfo as EventInfo)?.Invoke();
            }
        }

        /// <summary>
        /// 触发有参事件  游戏事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public void TriggerEvent<T>(GameEvent key, T value)
        {
            foreach (var eventData in _gameEventDic.SelectMany(ev => ev.Value.Where(eventData => eventData.GameEvent == key)))
            {
                (eventData.EventInfo as EventInfo<T>)?.Invoke(value);
            }
        }

        /// <summary>
        /// 触发有参事件  UI事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public void TriggerEvent<T>(UIEvent key, T value)
        {
            foreach (var eventData in _uiEventDic.SelectMany(ev => ev.Value.Where(eventData => eventData.UIEvent == key)))
            {
                (eventData.EventInfo as EventInfo<T>)?.Invoke(value);
            }
        }

        private void OnDestroy()
        {
            _gameEventDic.Clear();
            _uiEventDic.Clear();
        }
    }
}
