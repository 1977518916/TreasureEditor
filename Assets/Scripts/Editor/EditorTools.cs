using System.Collections.Generic;
using QFramework;
using Runtime.Data;
using Runtime.Manager;
using Spine.Unity;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 编辑器内工具集
/// </summary>
public class EditorTools
{
#if UNITY_EDITOR

    private static Dictionary<EntityModelType, string> AttackSpineDic = new Dictionary<EntityModelType, string>();

    private static Dictionary<string, string> SkillSpineDic = new Dictionary<string, string>();

    private static Dictionary<EntityModelType, string> CommonSpineDic = new Dictionary<EntityModelType, string>();

    private static ResLoader resLoader = ResLoader.Allocate();
    
    /// <summary>
    /// 动画数据文件路径收集
    /// </summary>
    [MenuItem("插件/收集所有动画文件资源路径")]
    public static void SpineDataAssetPathCollection()
    {
        //DataManager.InitAllSpineData();

        //SaveAttackSpineDataAssetPath();

        ReadAttackSpineDataAssetPath();
    }
    
    /// <summary>
    /// 保存攻击动画文件路径
    /// </summary>
    private static void SaveAttackSpineDataAssetPath()
    {
        foreach (var keyValue in DataManager.GetEntityBulletSpineDic())
        {
            AttackSpineDic.Add(keyValue.Key,
                AssetDatabase.GetAssetPath(keyValue.Value).Replace("Assets/Resources/", "").Replace(".asset", ""));
        }

        ES3.Save(HelpTools.GetEnumValueName<DataType>(DataType.EntityBulletSpineData), AttackSpineDic,
            "Resources/DataFile/SpineData.es3");
    }
    
    private static void ReadAttackSpineDataAssetPath()
    {
        AttackSpineDic = ES3.Load(HelpTools.GetEnumValueName<DataType>(DataType.EntityBulletSpineData),
            "Resources/DataFile/SpineData.es3", AttackSpineDic);
        Debug.Log($"{AttackSpineDic[EntityModelType.MaDai]}");
        var test = Resources.Load<SkeletonDataAsset>(AttackSpineDic[EntityModelType.MaDai]
            .Replace("Assets/Resources/", "").Replace(".asset", ""));
        Debug.Log($"{test}");
    }

#endif
}
