using QFramework;
using UnityEngine;

namespace Runtime.Utils
{
    public class ReadWriteUtility : IUtility
    {
        public T Read<T>(string key, T defaultValue = null) where T : class
        {
            string content = PlayerPrefs.GetString(key, "");
            if(content.Length < 1)
            {
                return defaultValue;
            }

            return JsonUtility.FromJson<T>(content);
        }

        public void Write(string key, object value)
        {
            string valueStr = JsonUtility.ToJson(value, true);
            Write(key, valueStr);
        }
    }
}