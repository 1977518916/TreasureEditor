using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<RectTransform> BattleBaseList = new List<RectTransform>();
    
    [Command]
    private void SetPrefabLocation(GameObject root, int value)
    {
        var obj = Instantiate(root, transform);
        obj.GetComponent<RectTransform>().position = BattleBaseList[value].position;
    }
}
