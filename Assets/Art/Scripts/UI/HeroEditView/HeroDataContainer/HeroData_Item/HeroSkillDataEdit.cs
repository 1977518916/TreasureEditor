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
		}

		private void InitSkillBtn()
		{
			SkillOne_Select_Btn.onClick.AddListener(() => UIKit.OpenPanel<SkillSelectPopUI>(UILevel.PopUI,
				new SkillSelectPopUIData
				{
					ClickAction = skillName =>
					{
						data.skillData1 = DataManager.GetSkillStruct().GetSkillDataOfKey(skillName);
						if (ResLoaderTools.TryGetEntitySkillSpineDataAsset(skillName, out var dataAsset))
						{
							SkillOneView.skeletonDataAsset = dataAsset;
							SkillOneView.Initialize(true);
						}
						else
						{
							throw new Exception($"报错：此{skillName} 技能Key找不到对应的技能动画");
						}
						
						UIKit.HidePanel<SkillSelectPopUI>();
					}
				}));

			SkillTwo_Select_Btn.onClick.AddListener(() => UIKit.OpenPanel<SkillSelectPopUI>(UILevel.PopUI,
				new SkillSelectPopUIData
				{
					ClickAction = skillName =>
					{
						data.skillData2 = DataManager.GetSkillStruct().GetSkillDataOfKey(skillName);
						if (ResLoaderTools.TryGetEntitySkillSpineDataAsset(skillName, out var dataAsset))
						{
							SkillTwoView.skeletonDataAsset = dataAsset;
							SkillTwoView.Initialize(true);
						}
						else
						{
							throw new Exception($"报错：此{skillName} 技能Key找不到对应的技能动画");
						}
						UIKit.HidePanel<SkillSelectPopUI>();
					}
				}));
		}
	}
}