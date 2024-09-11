using Runtime.Extensions;
using Runtime.Manager;
using Tao_Framework.Core.Singleton;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class CommonUi : MonoSingleton<CommonUi>
    {
        private void Awake()
        {
            //transform.FindGet<Button>("EnterLevel").onClick.AddListener();
        }
    }
}