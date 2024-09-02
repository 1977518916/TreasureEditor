using QFSW.QC;
using Runtime.Manager;
using Tao_Framework.Core.Singleton;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    /// <summary>
    /// 显示帧率
    /// </summary>
    public GameObject showFPS;

    /// <summary>
    /// 控制台程序
    /// </summary>
    public GameObject quantumConsole;
    
    /// <summary>
    /// 录制
    /// </summary>
    public GameObject recording;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            quantumConsole.SetActive(!quantumConsole.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            recording.SetActive(!recording.activeSelf);
        }
    }

    /// <summary>
    /// 是否显示帧率
    /// </summary>
    [Command]
    private void IsShowFPS(bool isShow)
    {
        showFPS.SetActive(isShow);
    }
}
