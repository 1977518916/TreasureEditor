using System;
using UnityEngine;

namespace Tao_Framework.Core.Event
{
    public interface IEventInfo
    {

    }

    /// <summary>
    /// 事件信息
    /// </summary>
    public class EventInfo : IEventInfo
    {
        private Action _action;

        public EventInfo(Action action)
        {
            _action += action;
        }

        public void Add(Action action)
        {
            _action += action;
        }

        public void Clear()
        {
            _action = null;
        }

        public void Invoke()
        {
            _action.Invoke();
        }
    }

    /// <summary>
    /// 事件信息
    /// </summary>
    /// <typeparam name="T">参数</typeparam>
    public class EventInfo<T> : IEventInfo
    {
        private Action<T> _action;

        public EventInfo(Action<T> action)
        {
            _action += action;
        }

        public void Add(Action<T> action)
        {
            _action += action;
        }

        public void Clear()
        {
            _action = null;
        }

        public void Invoke(T value)
        {
            _action.Invoke(value);
        }
    }

    /// <summary>
    /// 游戏事件
    /// </summary>
    public enum  GameEvent
    {
        /// <summary>
        /// 普通事件
        /// </summary>
        Common,

        /// <summary>
        /// 飞机开火事件
        /// </summary>
        Fire,

        /// <summary>
        /// 停止开火
        /// </summary>
        StopFire,

        /// <summary>
        /// 恢复镜头
        /// </summary>
        RestoreFootage,

        /// <summary>
        /// 恢复速度
        /// </summary>
        RecoverySpeed,

        /// <summary>
        /// 升级大小事件
        /// </summary>
        UpgradeScale,

        /// <summary>
        /// 启动升级大小事件计时器
        /// </summary>
        StartUpgradeScale,

        /// <summary>
        /// 停止生成怪物
        /// </summary>
        StopSpawnMonster,

        /// <summary>
        /// 开始生成怪物
        /// </summary>
        StartSpawnMonster,

        /// <summary>
        /// 提升攻击
        /// </summary>
        BoostAttack,

        /// <summary>
        /// 提升攻速
        /// </summary>
        BoostAttackSpeed,

        /// <summary>
        /// 减少怪物生成的时间
        /// </summary>
        ReduceMonsterGenerationTime,

        /// <summary>
        /// 停止生成怪物并且清除所有当前存在的怪物
        /// </summary>
        StopSpawnMonsterAndClearAllMonsters,

        /// <summary>
        /// 倒退
        /// </summary>
        Regress,

        /// <summary>
        /// 停止倒退
        /// </summary>
        StopRegress,

        /// <summary>
        /// 无敌
        /// </summary>
        Invincible,

        /// <summary>
        /// 解除无敌
        /// </summary>
        QuitInvincibility,

        /// <summary>
        /// 升级开火
        /// </summary>
        UpgradeFire,

        /// <summary>
        /// 升级辅助飞机
        /// </summary>
        UpgradePlane,

        /// <summary>
        /// 血量无敌
        /// </summary>
        HpInvincible,

        /// <summary>
        /// 技能开始进入CD
        /// </summary>
        AttackStartCd,

        /// <summary>
        /// 技能结束CD
        /// </summary>
        AttackEndCd,
		/// <summary>
        /// 生成敌人
        /// </summary>
        MakeEnemy,
		/// <summary>
        /// 实体死亡事件
        /// </summary>
        EntityDead
    }

    public enum UIEvent
    {
        /// <summary>
        /// UI普通事件
        /// </summary>
        UICommon
    }
}
