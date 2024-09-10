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

namespace QFramework.Example
{
	public partial class HeroSelect : UIElement
	{
		private void Awake()
		{
		}

		protected override void OnBeforeDestroy()
		{
		}

		public void InitView(HeroData heroData)
		{
			data = heroData;
			InitHeroView();
			InitButton();
		}

		/// <summary>
		/// 初始化英雄视图
		/// </summary>
		/// <exception cref="Exception"></exception>
		private void InitHeroView()
		{
			HeroView.gameObject.SetActive(data.modelType != EntityModelType.Null);
			HeroView.transform.localScale = data.modelType == EntityModelType.DongZhuo
				? new Vector3(0.2f, 0.2f, 1f)
				: new Vector3(0.5f, 0.5f, 1f);
			if (ResLoaderTools.TryGetEntityCommonSpineDataAsset(data.modelType, out var dataAsset))
			{
				SpineTools.SkeletonDataAssetReplace(HeroView, dataAsset);
			}
			else
			{
				if (data.modelType == EntityModelType.Null) return;
				throw new Exception($"报错：此{data.modelType} 实体枚举对应的Model缺失,请注意查找错误");
			}
		}

		/// <summary>
		/// 初始化按钮
		/// </summary>
		/// <exception cref="Exception"></exception>
		private void InitButton()
		{
			HeroSelect_Btn.onClick.AddListener(() =>
			{
				// 打开选择英雄的页面 并绑定对应的事件
				UIKit.OpenPanel<HeroSelectPopUI>(UILevel.PopUI, new HeroSelectPopUIData
				{
					ClickAction = modelType =>
					{
						data.modelType = modelType;
						HeroView.gameObject.SetActive(modelType != EntityModelType.Null);
						HeroView.transform.localScale = data.modelType == EntityModelType.DongZhuo
							? new Vector3(0.2f, 0.2f, 1f)
							: new Vector3(0.5f, 0.5f, 1f);
						if (ResLoaderTools.TryGetEntityCommonSpineDataAsset(data.modelType, out var dataAsset))
						{
							SpineTools.SkeletonDataAssetReplace(HeroView, dataAsset, "Idle");
						}
						else
						{
							throw new Exception($"报错：此{data.modelType} 实体枚举对应的Model缺失,请注意查找错误");
						}
					}
				});
			});
		}
	}
}