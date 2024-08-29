using System.IO;
using Spine.Unity;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class SkillNameModify
    {
        [MenuItem("插件/技能资源名称修改")]
        private static void ModifyTexture()
        {
            string modifyName = "skill_";
            SkeletonDataAsset[] skeletonDataAsset = Resources.LoadAll<SkeletonDataAsset>("");
            int index = 1;
            foreach (SkeletonDataAsset dataAsset in skeletonDataAsset)
            {
                if(dataAsset.name.ToLower().Contains("skill"))
                {
                    string path = AssetDatabase.GetAssetPath(dataAsset);
                    if(File.Exists(path))
                    {
                        File.Move(path, path.Replace(dataAsset.name, $"{modifyName}{index}"));
                        dataAsset.name = $"{modifyName}{index}";
                        index++;
                    }
                }
            }
        }

    }
}