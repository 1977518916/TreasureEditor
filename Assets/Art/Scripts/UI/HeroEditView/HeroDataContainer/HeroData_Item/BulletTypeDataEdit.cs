/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;
using TMPro;

namespace QFramework.Example
{
	public partial class BulletTypeDataEdit : UIElement
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
		private void InitView(HeroData heroData)
		{
			data = heroData;
			InitBulletType();
			InitBulletDropdownEvent();
		}
		
		private void InitBulletType()
		{
			foreach (var item in Enum.GetNames(typeof(BulletType)))
			{
				BulletType_Select.options.Add(new TMP_Dropdown.OptionData(item));
			}

			foreach (var item in Enum.GetNames(typeof(BulletAttributeType)))
			{
				BulletAttribute_Select.options.Add(new TMP_Dropdown.OptionData(item));
			}
		}
		
		private void InitBulletDropdownEvent()
		{
			BulletType_Select.onValueChanged.AddListener(value => data.bulletType = (BulletType)value);
			BulletAttribute_Select.onValueChanged.AddListener(value => data.bulletAttributeType = (BulletAttributeType)value);
		}
	}
}