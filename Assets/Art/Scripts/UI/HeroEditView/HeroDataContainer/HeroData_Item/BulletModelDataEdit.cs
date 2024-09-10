/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;
using Runtime.Manager;
using Spine.Unity;

namespace QFramework.Example
{
	public partial class BulletModelDataEdit : UIElement
	{
		private void Awake()
		{
		}

		protected override void OnBeforeDestroy()
		{
		}

		public void InitView(HeroData heroData)
		{
			BulletSpiteView.gameObject.SetActive(false);
			BulletSpineView.gameObject.SetActive(false);
			if (!DataManager.EntityIsHaveBullet(heroData.modelType)) return;
			if (ResLoaderTools.TryGetEntityBulletSpineDataAsset(heroData.modelType, out var dataAsset))
			{
				SetBulletModel(dataAsset);
			}
			SetBulletModelBtnCanClick(false);
		}

		/// <summary>
		/// 设置子弹模型选择按钮是否可以点击
		/// </summary>
		/// <param name="isCan"></param>
		private void SetBulletModelBtnCanClick(bool isCan)
		{
			BulletModelSelect_Btn.interactable = isCan;
		}
		
		/// <summary>
		/// 设置子弹模型
		/// ------------ 需要留给选择子弹类型的下拉框事件中调用 ------------
		/// </summary>
		private void SetBulletModel(SkeletonDataAsset dataAsset)
		{
			SpineTools.SkeletonDataAssetReplace(BulletSpineView, dataAsset);
			BulletSpineView.gameObject.SetActive(true);
		}
	}
}