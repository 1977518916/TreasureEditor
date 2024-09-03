using QFSW.QC;
using RenderHeads.Media.AVProMovieCapture;
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
    public CaptureGUI recordingGUI;
    
    /// <summary>
    /// 战斗界面画布
    /// </summary>
    public Canvas BattleCanvas;
    
    /// <summary>
    /// 战斗界面相机
    /// </summary>
    public Camera BattleCamera;

    public Transform fire;
    public Transform bullet;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            quantumConsole.SetActive(!quantumConsole.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            recordingGUI.gameObject.SetActive(!recordingGUI.gameObject.activeSelf);
            recordingGUI.ShowUI = true;
        }

        SetBattleCanvasCameraRender();

        if (Input.GetMouseButtonDown(0))
        {
            
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
    
    /// <summary>
    /// 设置战斗界面画布的相机渲染
    /// </summary>
    private void SetBattleCanvasCameraRender()
    {
        BattleCanvas.worldCamera = BattleCamera;
    }
}
