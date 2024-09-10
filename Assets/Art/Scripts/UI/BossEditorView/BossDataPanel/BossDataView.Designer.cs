/****************************************************************************
 * 2024.9 DESKTOP-426MFOR
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;

namespace QFramework.Example
{
	public partial class BossDataView
	{
		[SerializeField] public TMPro.TMP_InputField GenerateTimeInputField;
		[SerializeField] public TMPro.TMP_InputField SizeInputField;
		[SerializeField] public TMPro.TMP_InputField BossAttackInputField;
		[SerializeField] public TMPro.TMP_InputField BossHpInputField;
		[SerializeField] public TMPro.TMP_Dropdown BossBulletTypeDrop;
		[SerializeField] public TMPro.TMP_InputField BossRunSpeedInputField;

		private BossData data;
		
		public void Clear()
		{
			GenerateTimeInputField = null;
			SizeInputField = null;
			BossAttackInputField = null;
			BossHpInputField = null;
			BossBulletTypeDrop = null;
			BossRunSpeedInputField = null;
		}

		public override string ComponentName
		{
			get { return "BossDataView";}
		}
	}
}
