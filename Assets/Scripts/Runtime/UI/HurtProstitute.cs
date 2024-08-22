using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HurtProstitute : MonoBehaviour
{
    public void SetData(Vector2 location, int hurt, float surviveTime)
    {
        var rectTransform = GetComponent<RectTransform>();
        GetComponent<TextMeshProUGUI>().color = Color.white;
        rectTransform.position = new Vector2(location.x, location.y + 25f);
        rectTransform.DOMoveY(rectTransform.position.y + 50f, surviveTime);
        GetComponent<TextMeshProUGUI>().text = hurt.ToString();
        GetComponent<TextMeshProUGUI>().DOFade(0, surviveTime).onComplete += () => { Destroy(gameObject); };
    }
}
