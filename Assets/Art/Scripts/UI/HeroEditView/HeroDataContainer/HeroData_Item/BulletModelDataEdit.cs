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
			DataManager.GetSpecifyEntityBulletSpine(heroData.modelType, out var dataAsset);
			SetBulletModel(dataAsset);
			SetBulletModelBtnCanClick(false);
		}

		/// <summary>
		/// 设置子弹模型选择按钮是否可以点击
		/// </summary>
		/// <param name="isCan"></param>
		public void SetBulletModelBtnCanClick(bool isCan)
		{
			BulletModelSelect_Btn.interactable = isCan;
		}
		
		/// <summary>
		/// 初始化子弹模型按钮事件
		/// </summary>
		private void InitBulletModelBtnEvent()
		{
			BulletModelSelect_Btn.onClick.AddListener(() =>
			{
				// 弹出外部自定义子弹模型列表选择自定义子弹
			});
		}

		/// <summary>
		/// 设置子弹模型
		/// ------------ 需要留给选择子弹类型的下拉框事件中调用 ------------
		/// </summary>
		public void SetBulletModel(SkeletonDataAsset dataAsset)
		{
			BulletSpineView.skeletonDataAsset = dataAsset;
			BulletSpineView.gameObject.SetActive(true);
		}
		
		/// <summary>
		/// 设置子弹模型
		/// </summary>
		/// <param name="sprite"></param>
		public void SetBulletModel(Sprite sprite)
		{
			BulletSpiteView.sprite = sprite;
			BulletSpiteView.gameObject.SetActive(true);
		}
	}
}