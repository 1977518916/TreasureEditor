using UnityEngine;

public static class LocationCalculateTools
{
    /// <summary>
    /// 转换自身屏幕坐标位置到本地坐标位置
    /// </summary>
    /// <param name="self"> 自身位置组件 </param>
    /// <param name="canvasCamera"> 用于转换位置的相机 </param>
    /// <returns></returns>
    public static Vector2 ScreenToLocalPoint(this RectTransform self, Camera canvasCamera)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(self, RectTransformUtility.WorldToScreenPoint(
            canvasCamera, self.position), GameManager.Instance.BattleCanvas.worldCamera, out var converterLocation);
        return converterLocation;
    }

    /// <summary>
    /// 使用本地坐标域转换目标屏幕坐标位置到本地坐标位置
    /// </summary>
    /// <param name="self"> 要转换的目标本地坐标域</param>
    /// <param name="canvasCamera"> 用于转换位置的相机 </param>
    /// <param name="converterPoint"> 要转换的位置点 </param>
    /// <returns></returns>
    public static Vector2 ScreenToLocalPoint(this RectTransform self, Camera canvasCamera, Vector3 converterPoint)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(self, RectTransformUtility.WorldToScreenPoint(
            canvasCamera, converterPoint), GameManager.Instance.BattleCanvas.worldCamera, out var converterLocation);
        return converterLocation;
    }
}
