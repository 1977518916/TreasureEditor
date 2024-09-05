/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class HeroDataContainer
	{
		[SerializeField] public HeroData_Item HeroData_Item;

		public void Clear()
		{
			HeroData_Item = null;
		}

		public override string ComponentName
		{
			get { return "HeroDataContainer";}
		}
	}
}
