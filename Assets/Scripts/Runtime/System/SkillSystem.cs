using Runtime.Data;
using Runtime.Manager;
using Spine.Unity;
using Tao_Framework.Core.Singleton;
using UnityTimer;

namespace Runtime.System
{
    public class SkillSystem : MonoSingleton<SkillSystem>
    {
        public void ShowSkill(float triggerTime, SkillData skillData, HeroEntity entity)
        {
            Timer.Register(triggerTime, () =>
            {
                // SkeletonGraphic skeletonGraphic = AssetsLoadManager.LoadBulletSkeletonOfEnum()
                //todo 获取动画文件->执行具体逻辑
            });
        }
    }
}