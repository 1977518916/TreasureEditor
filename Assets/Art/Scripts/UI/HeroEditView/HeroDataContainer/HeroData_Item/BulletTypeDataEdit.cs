/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;
using Runtime.Utils;
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
		public void InitView(HeroData heroData)
		{
			data = heroData;
			InitBulletType();
			InitBulletDropdownEvent();
		}
		
		/// <summary>
		/// 初始化子弹类型
		/// </summary>
		private void InitBulletType()
		{
			foreach (BulletType item in Enum.GetValues(typeof(BulletType)))
			{
				BulletType_Select.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(item)));
			}

			foreach (BulletAttributeType item in Enum.GetValues(typeof(BulletAttributeType)))
			{
				BulletAttribute_Select.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(item)));
			}
		}
		
		/// <summary>
		/// 初始化子弹下拉框事件
		/// </summary>
		private void InitBulletDropdownEvent()
		{
			BulletType_Select.onValueChanged.AddListener(value => data.bulletType = (BulletType)value);
			BulletAttribute_Select.onValueChanged.AddListener(value => data.bulletAttributeType = (BulletAttributeType)value);
		}
	}
}