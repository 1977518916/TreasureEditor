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
			HeroHpValue_Input.SetTextWithoutNotify(heroData.hp.ToString());
			HeroAtkValue_Input.SetTextWithoutNotify(heroData.atk.ToString());
			HeroHpValue_Input.onValueChanged.AddListener(value => heroData.hp = Convert.ToInt32(value));
			HeroAtkValue_Input.onValueChanged.AddListener(value => heroData.atk = Convert.ToInt32(value));
		}
	}
}