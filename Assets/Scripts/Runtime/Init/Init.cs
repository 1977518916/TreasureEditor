using System;
using System.Collections;
using QFramework;
using QFramework.Example;
using Runtime.Manager;
using UnityEngine;

public class Init : MonoBehaviour
{
    private void Awake()
    {
        ResKit.Init();
        DataManager.Init(() => UIKit.OpenPanel<EditorUI>());
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        
    }
}
