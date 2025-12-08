using UnityEngine;

/// <summary>
/// HeaderとTooltipの機能を両方とも兼ね備えたカスタム属性クラス
/// </summary>
public class HeaderTooltipAttribute : PropertyAttribute
{
    //説明
    public readonly string Explanation;

    public HeaderTooltipAttribute(string explanation)
    {
        Explanation = explanation;
    }
}
