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
	public partial class HeroSkillDataEdit : UIElement
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
			InitSkillBtn();
			InitSkillView();
		}

		/// <summary>
		/// 初始化技能视图
		/// </summary>
		private void InitSkillView()
		{
			SkillOneView.gameObject.SetActive(false);
			SkillTwoView.gameObject.SetActive(false);
			SetSkillView(SkillOneView, data.skillData1);
			SetSkillView(SkillTwoView, data.skillData2);
		}
		
		private void InitSkillBtn()
		{
			SkillOne_Select_Btn.onClick.AddListener(() => UIKit.OpenPanel<SkillSelectPopUI>(UILevel.PopUI,
				new SkillSelectPopUIData
				{
					ClickAction = skillName =>
					{
						data.skillData1 = DataManager.GetSkillStruct().GetSkillDataOfKey(skillName);
						SetSkillView(SkillOneView, data.skillData1);
						UIKit.HidePanel<SkillSelectPopUI>();
					}
				}));

			SkillTwo_Select_Btn.onClick.AddListener(() => UIKit.OpenPanel<SkillSelectPopUI>(UILevel.PopUI,
				new SkillSelectPopUIData
				{
					ClickAction = skillName =>
					{
						data.skillData2 = DataManager.GetSkillStruct().GetSkillDataOfKey(skillName);
						SetSkillView(SkillTwoView, data.skillData2);
						UIKit.HidePanel<SkillSelectPopUI>();
					}
				}));
		}

		/// <summary>
		/// 设置技能视图
		/// </summary>
		/// <param name="graphic"></param>
		/// <param name="skillData"></param>
		private void SetSkillView(SkeletonGraphic graphic, SkillData skillData)
		{
			graphic.gameObject.SetActive(skillData != null);
			if (skillData == null || skillData.key.IsNullOrEmpty()) return;
			var skillScale = 0.5f;
			skillScale = skillData.showScale;
			var location = skillData.showPosition;
			var skillSpineTransform = graphic.transform;
			skillSpineTransform.localScale = new Vector3(skillScale / 2, skillScale / 2, 1);
			skillSpineTransform.localPosition = location;
			if (ResLoaderTools.TryGetEntitySkillSpineDataAsset(skillData.key, out var dataAsset))
			{
				SpineTools.SkeletonDataAssetReplace(graphic, dataAsset);
			}
			else
			{
				throw new Exception($"报错：此{skillData.key} 技能Key找不到对应的技能动画");
			}
		}
	}
}