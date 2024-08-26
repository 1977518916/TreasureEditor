using System.IO;
using Spine.Unity;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class AtkNameCaseConverter
    {

        [MenuItem("插件/一键攻击特效命名小写")]
        private static void CaseFile()
        {
            var vAssets = Resources.LoadAll<SkeletonDataAsset>("Effect");
            foreach (SkeletonDataAsset skeletonDataAsset in vAssets)
            {
                string path = AssetDatabase.GetAssetPath(skeletonDataAsset);
                if(File.Exists(path))
                {
                    File.Move(path, path.Replace(skeletonDataAsset.name, skeletonDataAsset.name.ToLower()));
                    skeletonDataAsset.name = skeletonDataAsset.name.ToLower();
                }
            }
        }
    }
}