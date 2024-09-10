/****************************************************************************
 * 2024.9 DESKTOP-426MFOR
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;
using Runtime.Utils;
using TMPro;

namespace QFramework.Example
{
	public partial class BossDataPanel : UIElement
	{
		private void Awake()
		{
		}

		protected override void OnBeforeDestroy()
		{
		}
		
		private void InitView(BossData bossData)
		{
			data = bossData;
			InitSelectBossDrop();
			if (data == null) return;
			SetBossModel(data.EntityModelType);
			if (data.BulletType == BulletType.Self) 
			{
				SetBossBullet(data.EntityModelType);
			}
		}

		private void InitSelectBossDrop()
		{
			if (data == null)
			{
				BossModelDrop.options.Add(new TMP_Dropdown.OptionData("无"));
			}
			else
			{
				foreach (EntityModelType item in Enum.GetValues(typeof(EntityModelType)))
				{
					if (item == EntityModelType.Null) continue;
					if (item > EntityModelType.DongZhuo) break;
					BossModelDrop.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(item)));
				}

				BossModelDrop.onValueChanged.AddListener(value =>
				{
					var valueToText = BossModelDrop.options[value].text;
					var modelType = (EntityModelType)Enum.Parse(typeof(EntityModelType), valueToText);
					if (modelType == EntityModelType.Null) return;
					data.EntityModelType = modelType;
					SetBossModel(modelType);
					SetBossBullet(modelType);
				});
			}
		}

		/// <summary>
		/// 设置Boss模型
		/// </summary>
		private void SetBossModel(EntityModelType modelType)
		{
			BossModel.gameObject.SetActive(modelType != EntityModelType.Null);
			if (modelType == EntityModelType.Null) return;
			if (ResLoaderTools.TryGetEntityCommonSpineDataAsset(modelType,out var dataAsset))
			{
				SpineTools.SkeletonDataAssetReplace(BossModel, dataAsset);
			}
			else
			{
				throw new Exception($"报错:此{modelType} 没有对应模型动画,请排查错误");
			}
		}

		/// <summary>
		/// 设置Boss子弹
		/// </summary>
		/// <param name="modelType"></param>
		private void SetBossBullet(EntityModelType modelType)
		{
			BossBulletModel.gameObject.SetActive(modelType != EntityModelType.Null);
			if (modelType == EntityModelType.Null) return;
			if (ResLoaderTools.TryGetEntityBulletSpineDataAsset(modelType, out var dataAsset)) 
			{
				SpineTools.SkeletonDataAssetReplace(BossBulletModel, dataAsset);
			}
			else
			{
				BossBulletModel.gameObject.SetActive(false);
			}
		}
	}
}