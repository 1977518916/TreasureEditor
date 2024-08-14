using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Runtime.Extensions
{
    public static class TransformExtensions
    {
        public static void ClearChild(this Transform transform, Action<GameObject> action = null)
        {
            if(action == null)
            {
                action = Object.Destroy;
            }
            foreach (Transform t in transform)
            {
                action.Invoke(t.gameObject);
            }
        }
    }
}