using UnityEngine;

namespace Runtime.Extensions
{
    public static class TransformExtensions
    {
        public static void ClearChild(this Transform transform)
        {
            foreach (Transform t in transform)
            {
                UnityEngine.Object.Destroy(t.gameObject);
            }
        }
    }
}