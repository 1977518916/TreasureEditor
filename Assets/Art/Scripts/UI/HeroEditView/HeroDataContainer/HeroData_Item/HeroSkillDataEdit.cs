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
		}

		private void InitSkillBtn()
		{
			//SkillOne_Select_Btn.onClick.AddListener(UIKit.OpenPanel<>());
		}
	}
}