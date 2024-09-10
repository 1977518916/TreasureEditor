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
			foreach (EntityModelType item in Enum.GetValues(typeof(EntityModelType)))
			{
				if (item == EntityModelType.Null) continue;
				if (!DataManager.EntityIsHaveBullet(item)) continue;
					var heroItem = Instantiate(HeroSelectItem, Content.transform, false);
					heroItem.InitView(item);
					heroSelectItemList.Add(heroItem);
					heroItem.gameObject.SetActive(true);
			}
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			mData = uiData as HeroSelectPopUIData ?? new HeroSelectPopUIData();
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
