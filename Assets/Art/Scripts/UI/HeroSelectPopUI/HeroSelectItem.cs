/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;
using Spine.Unity;

namespace QFramework.Example
{
	public partial class HeroSelectItem : UIElement
	{
		private void Awake()
		{
		}

		protected override void OnBeforeDestroy()
		{
		}

		public void InitView(EntityModelType modelType)
		{
			if (ResLoaderTools.TryGetEntityCommonSpineDataAsset(modelType, out var dataAsset))
			{
				SpineTools.SkeletonDataAssetReplace(HeroView, dataAsset, "Idle");
			}
			else
			{
				throw new Exception($"报错：此{modelType} 对应的英雄动画文件缺失,请排查错误");
			}

			entityModelType = modelType;
			HeroView.transform.localScale = modelType == EntityModelType.DongZhuo
				? new Vector3(0.25f, 0.25f, 1f)
				: new Vector3(0.5f, 0.5f, 1f);
		}

		/// <summary>
		/// 设置选择按钮事件
		/// </summary>
		/// <param name="click"></param>
		public void SetSelectBtnEvent(Action<EntityModelType> click)
		{
			SelectBtn.onClick.RemoveAllListeners();
			SelectBtn.onClick.AddListener(() =>
			{
				click.Invoke(entityModelType);
				UIKit.HidePanel<HeroSelectPopUI>();
			});
		}
	}
}