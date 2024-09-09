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

    /// <summary>
    /// 动画数据文件路径收集
    /// </summary>
    [MenuItem("插件/收集所有动画文件资源路径")]
    public static void SpineDataAssetPathCollection()
    {
        //DataManager.InitAllSpineData();

        SaveAttackSpineDataAssetPath();
        SaveCommonSpineDataAssetPath();
        SaveSkillSpineDataAssetPath();
    }

    /// <summary>
    /// 保存攻击动画文件路径
    /// </summary>
    private static void SaveAttackSpineDataAssetPath()
    {
        foreach (var keyValue in DataManager.GetEntityBulletSpineDic())
        {
            AttackSpineDic.Add(keyValue.Key,
                AssetDatabase.GetAssetPath(keyValue.Value).Replace(new[]
                {
                    Config.SPINE_FILE_PREFIX_REPLACE,
                    Config.SPINE_FILE_SUFFIX_REPLACE
                }, ""));
        }

        ES3.Save(HelpTools.GetEnumValueName<DataType>(DataType.EntityBulletSpineData), AttackSpineDic,
            Config.SPINE_DATA_MAP_FILE_PATH);
    }

    /// <summary>
    /// 保存寻常动画文件路径
    /// </summary>
    private static void SaveCommonSpineDataAssetPath()
    {
        foreach (var keyValue in DataManager.GetEntityCommonSpineDic())
        {
            CommonSpineDic.Add(keyValue.Key, AssetDatabase.GetAssetPath(keyValue.Value).Replace(new[]
            {
                Config.SPINE_FILE_PREFIX_REPLACE,
                Config.SPINE_FILE_SUFFIX_REPLACE
            }, ""));
        }

        ES3.Save(HelpTools.GetEnumValueName<DataType>(DataType.EntityCommonSpineData), CommonSpineDic,
            Config.SPINE_DATA_MAP_FILE_PATH);
    }

    /// <summary>
    /// 保存技能动画文件路径
    /// </summary>
    private static void SaveSkillSpineDataAssetPath()
    {
        foreach (var keyValue in DataManager.GetAllEntitySkillSpine())
        {
            SkillSpineDic.Add(keyValue.Key, AssetDatabase.GetAssetPath(keyValue.Value).Replace(new[]
            {
                Config.SPINE_FILE_PREFIX_REPLACE,
                Config.SPINE_FILE_SUFFIX_REPLACE
            }, ""));
        }

        ES3.Save(HelpTools.GetEnumValueName<DataType>(DataType.EntitySkillSpineData), SkillSpineDic,
            Config.SPINE_DATA_MAP_FILE_PATH);
    }

    private static void ReadAttackSpineDataAssetPath()
    {
        AttackSpineDic = ES3.Load(HelpTools.GetEnumValueName<DataType>(DataType.EntityBulletSpineData),
            Config.SPINE_DATA_MAP_FILE_PATH, AttackSpineDic);
    }
    
#endif
}
