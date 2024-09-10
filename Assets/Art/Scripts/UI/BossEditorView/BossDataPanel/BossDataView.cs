/****************************************************************************
 * 2024.9 DESKTOP-426MFOR
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;
using Runtime.Manager;
using Runtime.Utils;
using TMPro;

namespace QFramework.Example
{
	public partial class BossDataView : UIElement
	{
		private void Awake()
		{
		}

		protected override void OnBeforeDestroy()
		{
		}

		public void InitView(BossData bossData)
		{
			data = bossData;
			if (bossData == null) return;
			InitGenerateTimeTable();
			InitSizeTable();
			InitBossAttackTable();
			InitBossHpTable();
			InitBossBulletTable();
			InitBossRunSpeedTable();
		}

		private void InitGenerateTimeTable()
		{
			GenerateTimeInputField.onValueChanged.AddListener(value =>
			{
				data.Time = Convert.ToSingle(value);
			});
		}

		private void InitSizeTable()
		{
			SizeInputField.onValueChanged.AddListener(value =>
			{
				var scale = Convert.ToSingle(value);
				data.modelScale = scale > 0 ? scale < 2 ? scale : 2f : 0.1f;
				SizeInputField.SetTextWithoutNotify($"{data.modelScale}");
			});
		}

		private void InitBossAttackTable()
		{
			BossAttackInputField.onValueChanged.AddListener(value =>
			{
				data.Atk = Convert.ToInt32(value);
			});
		}

		private void InitBossHpTable()
		{
			BossHpInputField.onValueChanged.AddListener(value =>
			{
				data.Hp = Convert.ToInt32(value);
			});
		}

		private void InitBossBulletTable()
		{
			if (DataManager.EntityIsHaveBullet(data.EntityModelType))
			{
				BossBulletTypeDrop.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(BulletType.Self)));
			}

			BossBulletTypeDrop.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(BulletType.NoEntity)));
			BossBulletTypeDrop.onValueChanged.AddListener(value =>
			{
				data.BulletType = (BulletType)Enum.Parse(typeof(BulletType), BossBulletTypeDrop.options[value].text);
			});
		}
		
		private void InitBossRunSpeedTable()
		{
			BossRunSpeedInputField.onValueChanged.AddListener(value => { data.RunSpeed = Convert.ToInt32(value); });
		}
	}
}