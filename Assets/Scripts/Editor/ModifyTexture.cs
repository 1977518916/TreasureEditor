using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEditor;
using UnityEngine;

public class ModifyTexture
{
    private static readonly int StraightAlphaInput = Shader.PropertyToID("_StraightAlphaInput");

    [MenuItem("插件/Spine资源归位")]
    public static void MatchTheCorrespondingResourceFile()
    {
        var skeletonDataList = Resources.LoadAll<SkeletonDataAsset>("");
        var skeletonAtlasList = Resources.LoadAll<SpineAtlasAsset>("");
        foreach (var skeletonDataAsset in skeletonDataList)
        {
            foreach (var spineAtlasAsset in skeletonAtlasList)
            {
                if(skeletonDataAsset.name.ToLower().Replace("_skeletondata", "").Equals(spineAtlasAsset.name.ToLower().Replace("_atlas", "")))
                {
                    skeletonDataAsset.atlasAssets[0] = spineAtlasAsset;
                }
            }
        }
        
        // var texture2ds = Resources.LoadAll<Texture2D>("");
        // foreach (Texture2D texture2d in texture2ds)
        // {
        //     string path = AssetDatabase.GetAssetPath(texture2d);
        //     TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
        //     if(textureImporter != null)
        //     {
        //         textureImporter.isReadable = true;
        //         textureImporter.alphaIsTransparency = true;
        //         AssetDatabase.ImportAsset(path);
        //     }
        // }
    }
}