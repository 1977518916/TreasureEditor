/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;

namespace QFramework.Example
{
	public partial class HeroAttributeData : UIElement
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
		/// <param name="heroData"></param>
		public void InitView(HeroData heroData)
		{
			data = heroData;
			HeroHpValue_Input.SetTextWithoutNotify($"{heroData.hp}");
			HeroAtkValue_Input.SetTextWithoutNotify($"{heroData.atk}");
			HeroHpValue_Input.onEndEdit.AddListener(value =>
			{
				value = value.IsNullOrEmpty() ? "20" : value;
				data.hp = Convert.ToInt32(value);
				HeroHpValue_Input.SetTextWithoutNotify($"{value}");
			});
			HeroAtkValue_Input.onEndEdit.AddListener(value =>
			{
				value = value.IsNullOrEmpty() ? "5" : value;
				data.atk = Convert.ToInt32(value);
				HeroAtkValue_Input.SetTextWithoutNotify($"{value}");
			});
		}
	}
}