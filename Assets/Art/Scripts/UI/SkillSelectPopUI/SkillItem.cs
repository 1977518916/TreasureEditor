/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Manager;
using Spine.Unity;

namespace QFramework.Example
{
	public partial class SkillItem : UIElement
	{
		private void Awake()
		{
		}

		protected override void OnBeforeDestroy()
		{
		}

		public void InitView(string key)
		{
			skillKey = key;
			gameObject.SetActive(true);
			InitSkillSpine();
		}

		/// <summary>
		/// 设置技能按钮点击事件
		/// </summary>
		/// <param name="click"></param>
		public void SetSkillBtnEvent(Action<string> click)
		{
			SkillSelectBtn.onClick.RemoveAllListeners();
			SkillSelectBtn.onClick.AddListener(() => click.Invoke(skillKey));
		}

		/// <summary>
		/// 初始化技能Spine
		/// </summary>
		private void InitSkillSpine()
		{
			if (ResLoaderTools.TryGetEntitySkillSpineDataAsset(skillKey, out var dataAsset))
			{

				SpineTools.SkeletonDataAssetReplace(SkillSpine, dataAsset);
			}
			else
			{
				throw new Exception($"报错：此{skillKey} 技能Key找不到对应的技能动画");
			}
			SkillSpine.raycastTarget = false;
			var skillData = DataManager.GetSkillStruct().GetSkillDataOfKey(skillKey);
			var skillScale = 0.5f;
			var location = Vector3.zero;
			if (skillData != null)
			{
				skillScale = skillData.showScale;
				location = skillData.showPosition;
			}

			var skillSpineTransform = SkillSpine.transform;
			skillSpineTransform.localScale = new Vector3(skillScale, skillScale, 1);
			skillSpineTransform.localPosition = location;
		}
	}
}