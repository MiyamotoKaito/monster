using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HeaderTooltipAttribute))]
public class HeaderTooltipDrawer : PropertyDrawer
{
    private const float HEADERHEIGHT = 22f;

    private const float SPACEBELOWHEADER = 2f;
}
