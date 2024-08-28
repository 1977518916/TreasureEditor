using System;
using Spine.Unity;
using UnityEngine;

public interface AnimationComponent : IComponent
{
    /// <summary>
    /// 动画组件
    /// </summary>
    public SkeletonGraphic SkeletonGraphic { set; }
    
    /// <summary>
    /// 获取动画组件
    /// </summary>
    /// <returns></returns>
    public SkeletonGraphic GetSkeletonGraphic();

    /// <summary>
    /// 改变动画
    /// </summary>
    public void ChangeAnima(StateType stateType, bool isLoop, Action action);
    
    /// <summary>
    /// 是否含有该动画
    /// </summary>
    /// <param name="stateType"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public bool HasAnimation(StateType stateType, Action<bool> action = null);
}
