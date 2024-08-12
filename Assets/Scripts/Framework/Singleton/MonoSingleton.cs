using UnityEngine;

namespace Tao_Framework.Core.Singleton
{
    /// <summary>
    /// Mono类的单例基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (typeof(T))
                    {
                        if (instance == null)
                        {
                            instance = FindObjectOfType<T>();
                            if (instance == null)
                            {
                                var go = new GameObject(typeof(T).Name);
                                instance = go.AddComponent<T>();
                            }
                        }
                    }
                }

                return instance;
            }
        }

        public static void DestroyInstance()
        {
            DestroyImmediate(instance);
            instance = null;
        }
    }
}
