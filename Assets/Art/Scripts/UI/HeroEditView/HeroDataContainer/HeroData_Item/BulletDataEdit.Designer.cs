/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;

namespace QFramework.Example
{
	public partial class BulletDataEdit
	{
		[SerializeField] public TMPro.TMP_InputField BulletMaxValue_Input;
		[SerializeField] public TMPro.TMP_InputField AttackInterval_Input;
		[SerializeField] public TMPro.TMP_InputField SingleBulletNumber_Input;

		private HeroData data;
		
		public void Clear()
		{
			BulletMaxValue_Input = null;
			AttackInterval_Input = null;
			SingleBulletNumber_Input = null;
		}

		public override string ComponentName
		{
			get { return "BulletDataEdit";}
		}
	}
}
