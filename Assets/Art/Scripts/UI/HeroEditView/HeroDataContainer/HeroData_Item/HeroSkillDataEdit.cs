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
						var skillScale = 0.5f;
						var location = Vector3.zero;
						if (data.skillData1 != null)
						{
							skillScale = data.skillData1.showScale;
							location = data.skillData1.showPosition;
						}

						var skillSpineTransform = SkillOneView.transform;
						skillSpineTransform.localScale = new Vector3(skillScale / 2, skillScale / 2, 1);
						skillSpineTransform.localPosition = location;
						if (ResLoaderTools.TryGetEntitySkillSpineDataAsset(skillName, out var dataAsset))
						{
							SpineTools.SkeletonDataAssetReplace(SkillOneView, dataAsset);
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
						var skillScale = 0.5f;
						var location = Vector3.zero;
						if (data.skillData2 != null)
						{
							skillScale = data.skillData2.showScale;
							location = data.skillData2.showPosition;
						}

						var skillSpineTransform = SkillTwoView.transform;
						skillSpineTransform.localScale = new Vector3(skillScale, skillScale, 1);
						skillSpineTransform.localPosition = location;
						if (ResLoaderTools.TryGetEntitySkillSpineDataAsset(skillName, out var dataAsset))
						{
							SpineTools.SkeletonDataAssetReplace(SkillTwoView, dataAsset);
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