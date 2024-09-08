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
			AttackInterval_Input.onValueChanged.AddListener(value => data.atkInterval = Convert.ToSingle(value));
			BulletMaxValue_Input.onValueChanged.AddListener(value => data.bulletAmount = Convert.ToInt32(value));
			SingleBulletNumber_Input.onValueChanged.AddListener(value => data.shooterAmount = Convert.ToInt32(value));
		}
	}
}