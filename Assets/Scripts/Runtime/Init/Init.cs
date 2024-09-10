using QFramework;
using QFramework.Example;
using Runtime.Manager;
using UnityEngine;

public class Init : MonoBehaviour
{
    private void Awake()
    {
        ResKit.Init();
        FileTools.Init();
        DataManager.Init(() => UIKit.OpenPanel<EditorUI>());
    }
}
