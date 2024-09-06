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
	public partial class HeroData_Item : UIElement
	{
		private void Awake()
		{
			
		}

		protected override void OnBeforeDestroy()
		{
		}

		/// <summary>
		/// 初始化视图数据
		/// </summary>
		public void InitView(int index, HeroData heroData)
		{
			data = heroData;
			InitDeleteBtn(index);
		}
		
		
		
		
		private void InitDeleteBtn(int index)
		{
			// 首个英雄无法被删除
			Delete_Btn.gameObject.SetActive(index != 0);
			// 删除按钮绑定的事件
			Delete_Btn.onClick.AddListener(() =>
			{
				DataManager.HeroDataList.RemoveAt(DataManager.HeroDataList.Count - 1);
				Destroy(gameObject);
			});
		}
	}
}