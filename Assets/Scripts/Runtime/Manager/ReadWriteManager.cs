using Runtime.Data;
using UnityEngine;

namespace Runtime.Manager
{
    public static class ReadWriteManager
    {

        public static HeroData GetHeroData(DataType.HeroPositionType heroPositionType)
        {
            string content = PlayerPrefs.GetString(heroPositionType.ToString(), "");
            if(content.Length < 1)
            {
                return new HeroData();
            }
            return JsonUtility.FromJson<HeroData>(content);
        }

        public static void SaveHeroData(DataType.HeroPositionType heroPositionType, HeroData heroData)
        {
            string content = heroData == null ? "" : JsonUtility.ToJson(heroData);
            PlayerPrefs.SetString(heroPositionType.ToString(), content);
            PlayerPrefs.Save();
        }

        public static T Load<T>() where T : class
        {
            string content = PlayerPrefs.GetString(typeof(T).ToString(), "");
            Debug.Log("type:" + typeof(T));
            Debug.Log("content:" + content);
            if(content.Length == 0)
            {
                return null;
            }
            return JsonUtility.FromJson<T>(content);
        }

        public static void Save(object t)
        {
            string content = JsonUtility.ToJson(t);
            PlayerPrefs.SetString(t.GetType().ToString(), content);
            Debug.Log("type:" + t.GetType());
            Debug.Log("content:" + content);
            PlayerPrefs.Save();
        }

        public static void Clear()
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public class DataType
    {
        public enum HeroPositionType
        {
            Hero1,
            Hero2,
            Hero3,
            Hero4,
            Hero5,
        }
    }
}