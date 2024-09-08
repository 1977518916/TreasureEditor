/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;

namespace QFramework.Example
{
	public partial class HeroModelScaleDataEdit
	{
		[SerializeField] public TMPro.TMP_InputField ModelScale_Input;

		private HeroData data;
		
		public void Clear()
		{
			ModelScale_Input = null;
		}

		public override string ComponentName
		{
			get { return "HeroModelScaleDataEdit";}
		}
	}
}
