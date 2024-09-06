/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class HeroSelect : UIElement
	{
		private void Awake()
		{
		}

		protected override void OnBeforeDestroy()
		{
		}
		
		public void InitView()
		{
			HeroSelect_Btn.onClick.AddListener(() =>
			{
				// 打开选择英雄的页面 并绑定对应的事件
			});
			
			
		}
	}
}