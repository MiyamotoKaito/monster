using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HeaderTooltipAttribute))]
public class HeaderTooltipDrawer : PropertyDrawer
{
    private const float HEADERHEIGHT = 22f;

    private const float SPACEBELOWHEADER = 2f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true) + HEADERHEIGHT + SPACEBELOWHEADER;
    }
}
