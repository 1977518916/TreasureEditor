/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;

namespace QFramework.Example
{
	public partial class HeroAttributeData
	{
		[SerializeField] public TMPro.TMP_InputField HeroHpValue_Input;
		[SerializeField] public TMPro.TMP_InputField HeroAtkValue_Input;

		private HeroData data;
		
		public void Clear()
		{
			HeroHpValue_Input = null;
			HeroAtkValue_Input = null;
		}
		
		public override string ComponentName
		{
			get { return "HeroAttributeData";}
		}
	}
}
