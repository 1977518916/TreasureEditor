using QFramework;
using Runtime.Data;
using Spine.Unity;
using UnityEngine;

namespace Factories
{
    public class BulletGameObjectFactory : Singleton<BulletGameObjectFactory>, IFactory
    {
        private BulletGameObjectFactory()
        {
            
        }
        public GameObject Create(EntityModelType modelType, int layer, Transform parent = null)
        {
            var bulletObj = new GameObject($"{modelType.ToString()}_Bullet", typeof(RectTransform))
            {
                layer = layer
            };
            bulletObj.transform.SetParent(parent);
            bulletObj.transform.localPosition = Vector3.zero;
            bulletObj.transform.localScale = new Vector3(2f, 2f, 1f);
            if(HelpTools.BulletIsSpine(modelType) && ResLoaderTools.TryGetEntityBulletSpineDataAsset(modelType, out SkeletonDataAsset asset))
            {
                var spine = SkeletonGraphicFactory.Instance.Create(asset).gameObject;
                spine.transform.eulerAngles = new Vector3(0, 0, -90f);
            }
            else
            {
                var bullet = ResLoaderTools.LoadPrefab(HelpTools.BulletPrefabPath(modelType));
                bullet.transform.SetParent(bulletObj.transform);
                bullet.transform.eulerAngles = new Vector3(0, 0, 90f);
            }
            return bulletObj;
        }
    }
}