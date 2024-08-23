using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HurtProstitute : MonoBehaviour
{
    public void SetData(Vector2 location, int hurt, float surviveTime)
    {
        var rectTransform = GetComponent<RectTransform>();
        GetComponent<Text>().color = Color.white;
        rectTransform.position = new Vector2(location.x, location.y + 25f);
        rectTransform.DOMoveY(rectTransform.position.y + 50f, surviveTime);
        GetComponent<Text>().text = hurt.ToString();
        GetComponent<Text>().DOFade(0, surviveTime).onComplete += () => { Destroy(gameObject); };
    }
}
