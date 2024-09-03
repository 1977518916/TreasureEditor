using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HurtText : MonoBehaviour
{
    public void SetData(Vector2 location, int hurt, float surviveTime)
    {
        var rectTransform = GetComponent<RectTransform>();
        GetComponent<Text>().color = Color.white;
        rectTransform.localScale = Vector3.one;
        rectTransform.anchoredPosition3D = new Vector3(location.x, location.y + 25f, 0f);
        rectTransform.DOLocalMoveY(rectTransform.localPosition.y + 50f, surviveTime);
        GetComponent<Text>().text = hurt.ToString();
        GetComponent<Text>().DOFade(0, surviveTime).onComplete += () => { Destroy(gameObject); };
    }
}
