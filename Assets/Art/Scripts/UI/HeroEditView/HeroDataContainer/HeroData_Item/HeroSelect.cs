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
			HeroSelect_Btn.onClick.AddListener(() =>
			{
				// 打开选择英雄的页面 并绑定对应的事件
				UIKit.OpenPanel<HeroSelectPopUI>(UILevel.PopUI, new HeroSelectPopUIData
				{
					ClickAction = modelType =>
					{
						data.modelType = modelType;
						HeroView.transform.localScale = data.modelType == EntityModelType.DongZhuo
							? new Vector3(0.2f, 0.2f, 1f)
							: new Vector3(0.5f, 0.5f, 1f);
						//HeroView.skeletonDataAsset = DataManager.GetAllEntityCommonSpine()[data.modelType];
						HeroView.Initialize(true);
					}
				});
			});
		}
	}
}