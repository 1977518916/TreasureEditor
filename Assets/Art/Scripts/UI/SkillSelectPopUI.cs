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
	public class SkillSelectPopUIData : UIPanelData
	{
		public Dictionary<string, SkeletonDataAsset> AllSkill;
		public HeroData Data;
		public Action<string> ClickAction;
	}
	public partial class SkillSelectPopUI : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as SkillSelectPopUIData ?? new SkillSelectPopUIData();
			// please add init code here
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			SkillItem.gameObject.SetActive(false);
			foreach (var item in mData.AllSkill.Values)
			{
				var skill = Instantiate(SkillItem, Content.transform, false);
				skill.InitView(item.name, item);
				skill.SetSkillBtnEvent(mData.ClickAction);
				skillItemList.Add(skill);
			}
		}
		
		protected override void OnShow()
		{
			SkillScrollView.normalizedPosition = new Vector2(0, 0);
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
		
		/// <summary>
		/// 当技能选择弹窗已经存在于UI栈中时 那么第二次打开之前需要重新根据当前点击传入新的点击事件
		/// </summary>
		/// <param name="click"></param>
		public void SetOnClickEvent(Action<string> click)
		{
			foreach (var item in skillItemList)
			{
				item.SetSkillBtnEvent(click);
			}
		}
	}
}
