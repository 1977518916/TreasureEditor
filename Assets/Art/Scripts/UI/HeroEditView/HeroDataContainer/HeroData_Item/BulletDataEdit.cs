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
	public partial class BulletDataEdit : UIElement
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
			AttackInterval_Input.SetTextWithoutNotify($"{data.atkInterval}");
			BulletMaxValue_Input.SetTextWithoutNotify($"{data.bulletAmount}");
			SingleBulletNumber_Input.SetTextWithoutNotify($"{data.shooterAmount}");
			AttackInterval_Input.onEndEdit.AddListener(value =>
			{
				value = value.IsNullOrEmpty() ? "1" : value;
				data.atkInterval = Convert.ToInt32(value);
				AttackInterval_Input.SetTextWithoutNotify($"{value}");
			});
			BulletMaxValue_Input.onEndEdit.AddListener(value =>
			{
				value = value.IsNullOrEmpty() ? "10" : value;
				data.bulletAmount = Convert.ToInt32(value);
				BulletMaxValue_Input.SetTextWithoutNotify($"{value}");
			});
			SingleBulletNumber_Input.onEndEdit.AddListener(value =>
			{
				value = value.IsNullOrEmpty() ? "1" : value;
				data.shooterAmount = Convert.ToInt32(value);
				SingleBulletNumber_Input.SetTextWithoutNotify($"{value}");
			});
		}
	}
}