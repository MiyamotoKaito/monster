using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HeaderTooltipAttribute))]
public class HeaderTooltipDrawer : PropertyDrawer
{
    //ヘッダーの高さを定義（標準のHeaderAttributeに合わせる）
    private const float HEADERHEIGHT = 22f;

    //ヘッダーとフィールドの間のスペース
    private const float SPACEBELOWHEADER = 2f;

    /// <summary>
    /// プロパティの描画に必要な全体の高さを計算します。
    /// </summary>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // 基本のプロパティの高さ + Headerの高さ + Headerとプロパティ間のスペース
        return EditorGUI.GetPropertyHeight(property, label, true) + HEADERHEIGHT + SPACEBELOWHEADER;
    }

    /// <summary>
    /// GUIを描画します。
    /// </summary>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 属性情報を取得
        HeaderTooltipAttribute att = (HeaderTooltipAttribute)attribute;

        //ヘッダーを描画するためのRectを計算
        Rect headerRect = new Rect(position.x, position.y, position.width, HEADERHEIGHT);

        //フィールドを描画するためのRectを計算
        Rect fieldRect = new Rect(position.x, position.y + HEADERHEIGHT + SPACEBELOWHEADER, 
                                  position.width, EditorGUIUtility.singleLineHeight);

        // --- ヘッダーの描画 ---
        // 標準のHeaderAttributeと同様のスタイルで描画（太字、中央揃えなど）
        EditorGUI.LabelField(headerRect, att.Explanation, EditorStyles.boldLabel);

        // --- フィールド（プロパティ）の描画 ---
        // Tooltipテキストをセット
        GUIContent content = new GUIContent(label.text, att.Explanation);

        // 標準のプロパティを描画
        EditorGUI.PropertyField(fieldRect, property, content, true);
    }
}
