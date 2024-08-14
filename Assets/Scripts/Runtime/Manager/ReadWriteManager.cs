using Runtime.Data;
using UnityEngine;

namespace Runtime.Manager
{
    public static class ReadWriteManager
    {
        public static T Read<T>(string key, T defaultValue = null) where T : class
        {
            string content = PlayerPrefs.GetString(key, "");
            if(content.Length < 1)
            {
                return defaultValue;
            }
            return JsonUtility.FromJson<T>(content);
        }

        public static void Write(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        public static void Clear()
        {
            PlayerPrefs.DeleteAll();
        }
        #region Hero

        public static class Hero
        {
            public static HeroData GetHeroData(DataType.HeroPositionType heroPositionType)
            {
                return Read(heroPositionType.ToString(), new HeroData());
            }

            public static void SaveHeroData(DataType.HeroPositionType heroPositionType, HeroData heroData)
            {
                string content = heroData == null ? "" : JsonUtility.ToJson(heroData);
                Write(heroPositionType.ToString(), content);
            }
        }

        #endregion
        #region LevelData

        public static class Level
        {
            public static LevelData GetLevelData()
            {
                return Read(nameof(Level), new LevelData());
            }

            public static void SaveLevelData(LevelData levelData)
            {
                string content = levelData == null ? "" : JsonUtility.ToJson(levelData);
                Write(nameof(Level), content);
            }
        }

        #endregion
    }

    public static class DataType
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