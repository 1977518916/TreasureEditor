using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;
using Runtime.Manager;

namespace QFramework.Example
{
	public class HeroSelectPopUIData : UIPanelData
	{
		public Action<EntityModelType> ClickAction;
	}
	public partial class HeroSelectPopUI : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HeroSelectPopUIData ?? new HeroSelectPopUIData();
			HeroSelectItem.gameObject.SetActive(false);
			foreach (var keyValue in DataManager.GetAllEntityCommonSpine())
			{
				if (keyValue.Key >= EntityModelType.XiaoBing_GongJian) break;
				var heroItem = Instantiate(HeroSelectItem, Content.transform, false);
				heroItem.InitView(keyValue.Key, keyValue.Value);
				heroSelectItemList.Add(heroItem);
				heroItem.gameObject.SetActive(true);
			}
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			foreach (var item in heroSelectItemList)
			{
				item.SetSelectBtnEvent(mData.ClickAction);
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
