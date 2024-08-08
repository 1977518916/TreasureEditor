using UnityEngine;

namespace Runtime.Extensions
{
    public static class ComponentExtensions
    {
        public static TMono FindAdd<TMono>(this Transform self, string path) where TMono : MonoBehaviour
        {
            Transform childTransform = self.Find(path);
            return childTransform.gameObject.AddComponent<TMono>();
        }
        
        public static TMono FindGet<TMono>(this Transform self, string path) where TMono : MonoBehaviour
        {
            Transform childTransform = self.Find(path);
            return childTransform.gameObject.GetComponent<TMono>();
        }
        
        public static TMono FindGetInChildren<TMono>(this Transform self, string path) where TMono : MonoBehaviour
        {
            Transform childTransform = self.Find(path);
            return childTransform.gameObject.GetComponentInChildren<TMono>();
        }
    }
}