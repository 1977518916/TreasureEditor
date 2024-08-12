namespace Framework.Singleton
{
    /// <summary>
    /// 普通类的单例基类
    /// </summary>
    public class Singleton<T> where T : class, new()
    {
        private static T _instance;

        private static readonly object syslock = new object();

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syslock)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
