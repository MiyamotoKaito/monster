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

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        HeaderTooltipAttribute att = (HeaderTooltipAttribute)attribute;

        Rect headerRect = new Rect(position.x, position.y, position.width, HEADERHEIGHT);

        Rect fieldRect = new Rect(position.x, position.y + HEADERHEIGHT + SPACEBELOWHEADER, 
                                  position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.LabelField(headerRect, att.Explanation, EditorStyles.boldLabel);

        GUIContent content = new GUIContent(label.text, att.Explanation);

        EditorGUI.PropertyField(fieldRect, property, content, true);
    }
}
