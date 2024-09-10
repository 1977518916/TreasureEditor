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
	public partial class HeroModelScaleDataEdit : UIElement
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
			ModelScale_Input.SetTextWithoutNotify($"{heroData.modelScale}");
			ModelScale_Input.onValueChanged.AddListener(value => heroData.modelScale = Convert.ToSingle(value));
			ModelScale_Input.onEndEdit.AddListener(value =>
			{
				var scaleValue = Convert.ToSingle(value);
				// 如果大于0 就看是不是小于2 如果小于等于2 就用这个值 如果大于2 就用2 如果不大于0 就用0.1f
				scaleValue = scaleValue > 0 ? scaleValue <= 2 ? scaleValue : 2 : 0.1f;
				heroData.modelScale = Convert.ToSingle(scaleValue);
			});
		}
	}
}