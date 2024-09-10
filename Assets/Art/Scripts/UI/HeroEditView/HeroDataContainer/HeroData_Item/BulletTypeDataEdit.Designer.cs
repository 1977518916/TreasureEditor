/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;

namespace QFramework.Example
{
	public partial class BulletTypeDataEdit
	{
		[SerializeField] public TMPro.TMP_Dropdown BulletType_Select;
		[SerializeField] public TMPro.TMP_Dropdown BulletAttribute_Select;
		[SerializeField] public UnityEngine.UI.Button BulletSelect_Btn;

		private HeroData data;
		
		public void Clear()
		{
			BulletType_Select = null;
			BulletAttribute_Select = null;
			BulletSelect_Btn = null;
		}

		public override string ComponentName
		{
			get { return "BulletTypeDataEdit";}
		}
	}
}
