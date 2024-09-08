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
	/// <summary>
	/// Init的时候不需要绑定对应的点击事件,因为会在每次OpenView时绑定
	/// </summary>
	public class SkillSelectPopUIData : UIPanelData
	{
		public Action<string> ClickAction;
	}
	public partial class SkillSelectPopUI : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as SkillSelectPopUIData ?? new SkillSelectPopUIData();
			SkillItem.gameObject.SetActive(false);
			foreach (var item in DataManager.AllEntitySkillSpineDic.Values)
			{
				var skill = Instantiate(SkillItem, Content.transform, false);
				skill.InitView(item.name, item);
				skillItemList.Add(skill);
				skill.gameObject.SetActive(true);
			}
		}

		protected override void OnOpen(IUIData uiData = null)
		{
			//SkillScrollView.normalizedPosition = new Vector2(0, 0);
			foreach (var item in skillItemList)
			{
				item.SetSkillBtnEvent(mData.ClickAction);
			}
		}

		protected override void OnShow()
		{
			
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
