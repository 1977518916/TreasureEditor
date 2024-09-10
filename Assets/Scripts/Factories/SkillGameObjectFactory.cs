using QFramework;
using Runtime.Data;
using Runtime.Manager;
using Spine.Unity;
using UnityEngine;

namespace Factories
{
    public class SkillGameObjectFactory : Singleton<SkillGameObjectFactory>, IFactory
    {
        private SkillGameObjectFactory()
        {

        }
        public SkeletonGraphic Create(SkillData skillData, Transform parent = null)
        {
            if(ResLoaderTools.TryGetEntitySkillSpineDataAsset(skillData.key, out var dataAsset))
            {
                SkeletonGraphic graphic = SkeletonGraphicFactory.Instance.Create(dataAsset, parent, skillData.isLoopPlay);
                graphic.transform.eulerAngles = new Vector3(0, 0, skillData.rotations);
                return graphic;
            }
            throw new System.Exception("Can't find entity skill spine");
        }
    }
}