using System;
using System.Collections.Generic;
using Tao_Framework.Core.Singleton;
using UnityTimer;

namespace Tao_Framework.Core.Event
{
    public class EventMgr : MonoSingleton<EventMgr>
    {
        /// <summary>
        /// 游戏事件 无参 字典
        /// </summary>
        private readonly Dictionary<GameEvent, IEventInfo> _gameEventDic = new Dictionary<GameEvent, IEventInfo>();

        /// <summary>
        /// UI事件字典
        /// </summary>
        private readonly Dictionary<UIEvent, IEventInfo> _uiEventDic = new Dictionary<UIEvent, IEventInfo>();

        /// <summary>
        /// 注册事件 游戏事件 无参
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public void RegisterEvent(GameEvent key, Action action)
        {
            if (_gameEventDic.TryGetValue(key, out var value))
            {
                (value as EventInfo)?.Add(action);
            }
            else
            {
                _gameEventDic.Add(key, new EventInfo(action));
            }
        }

        /// <summary>
        /// 注册事件 UI事件 无参
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public void RegisterEvent(UIEvent key, Action action)
        {
            if (_uiEventDic.TryGetValue(key, out var value))
            {
                (value as EventInfo)?.Add(action);
            }
            else
            {
                _uiEventDic.Add(key, new EventInfo(action));
            }
        }
        
        /// <summary>
        /// 注册事件 游戏事件 带参数的
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public void RegisterEvent<T>(GameEvent key, Action<T> action)
        {
            if (_gameEventDic.TryGetValue(key, out var value))
            {
                (value as EventInfo<T>)?.Add(action);
            }
            else
            {
                _gameEventDic.Add(key, new EventInfo<T>(action));
            }
        }
        
        /// <summary>
        /// 注册事件 UI事件 带参数的
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public void RegisterEvent<T>(UIEvent key, Action<T> action)
        {
            if (_uiEventDic.TryGetValue(key, out var value))
            {
                (value as EventInfo<T>)?.Add(action);
            }
            else
            {
                _uiEventDic.Add(key, new EventInfo<T>(action));
            }
        }

        /// <summary>
        /// 清除事件 但是不移除对应的key
        /// </summary>
        /// <param name="key"></param>
        public void ClearEvent(GameEvent key)
        {
            if (_gameEventDic.TryGetValue(key, out var value))
            {
                (value as EventInfo)?.Clear();
            }
        }

        /// <summary>
        /// 清除事件 但是不移除对应的key
        /// </summary>
        /// <param name="key"></param>
        public void ClearEvent(UIEvent key)
        {
            if (_uiEventDic.TryGetValue(key, out var value))
            {
                (value as EventInfo)?.Clear();
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="key"></param>
        public void RemoveEvent(GameEvent key)
        {
            if (!_gameEventDic.ContainsKey(key)) return;
            _gameEventDic.Remove(key);
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="key"></param>
        public void RemoveEvent(UIEvent key)
        {
            if (!_uiEventDic.ContainsKey(key)) return;
            _uiEventDic.Remove(key);
        }

        /// <summary>
        /// 触发无参事件
        /// </summary>
        /// <param name="key"></param>
        public void TriggerEvent(GameEvent key)
        {
            if (!_gameEventDic.TryGetValue(key, out var value)) return;
            (value as EventInfo)?.Invoke();
        }

        /// <summary>
        /// 延时无参事件触发
        /// </summary>
        /// <param name="key"></param>
        /// <param name="time"></param>
        public void TriggerEvent(GameEvent key, float time)
        {
            if (!_gameEventDic.TryGetValue(key, out var eventInfo)) return;
            Timer.Register(time, () => (eventInfo as EventInfo)?.Invoke());
        }

        /// <summary>
        /// 延时有参事件触发
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        public void TriggerEvent<T>(GameEvent key, T value, float time)
        {
            if (!_gameEventDic.TryGetValue(key, out var eventInfo)) return;
            Timer.Register(time, () => (eventInfo as EventInfo<T>)?.Invoke(value));
        }

        /// <summary>
        /// 触发无参事件
        /// </summary>
        /// <param name="key"></param>
        public void TriggerEvent(UIEvent key)
        {
            if (!_uiEventDic.TryGetValue(key, out var value)) return;
            (value as EventInfo)?.Invoke();
        }

        /// <summary>
        /// 触发有参事件  游戏事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public void TriggerEvent<T>(GameEvent key, T value)
        {
            if (!_gameEventDic.TryGetValue(key, out var eventInfo)) return;
            (eventInfo as EventInfo<T>)?.Invoke(value);
        }

        /// <summary>
        /// 触发有参事件  UI事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public void TriggerEvent<T>(UIEvent key, T value)
        {
            if (!_uiEventDic.TryGetValue(key, out var eventInfo)) return;
            (value as EventInfo<T>)?.Invoke(value);
        }
    }
}
