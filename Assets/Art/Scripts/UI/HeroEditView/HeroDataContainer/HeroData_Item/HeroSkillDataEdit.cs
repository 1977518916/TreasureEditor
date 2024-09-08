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
						data.skillData1 = DataManager.SkillStruct.GetSkillDataOfKey(skillName);
						SkillOneView.skeletonDataAsset = DataManager.AllEntitySkillSpineDic[skillName];
						UIKit.HidePanel<SkillSelectPopUI>();
					}
				}));

			SkillTwo_Select_Btn.onClick.AddListener(() => UIKit.OpenPanel<SkillSelectPopUI>(UILevel.PopUI,
				new SkillSelectPopUIData
				{
					ClickAction = skillName =>
					{
						data.skillData2 = DataManager.SkillStruct.GetSkillDataOfKey(skillName);
						SkillTwoView.skeletonDataAsset = DataManager.AllEntitySkillSpineDic[skillName];
						UIKit.HidePanel<SkillSelectPopUI>();
					}
				}));
		}
	}
}