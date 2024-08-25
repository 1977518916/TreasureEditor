using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEditor;
using UnityEngine;

public class ModifyTexture : MonoBehaviour
{
    private static readonly int StraightAlphaInput = Shader.PropertyToID("_StraightAlphaInput");

    [Button]
    private void MatchTheCorrespondingResourceFile()
    {
        var skeletonDataList = Resources.LoadAll<SkeletonDataAsset>("");
        var skeletonAtlasList = Resources.LoadAll<SpineAtlasAsset>("");
        foreach (var skeletonDataAsset in skeletonDataList)
        {
            foreach (var spineAtlasAsset in skeletonAtlasList)
            {
                if (skeletonDataAsset.name.Replace("_SkeletonData","").Equals(spineAtlasAsset.name.Replace("_Atlas", "")))
                {
                    skeletonDataAsset.atlasAssets[0] = spineAtlasAsset;
                }
            }
        }
    }
}
