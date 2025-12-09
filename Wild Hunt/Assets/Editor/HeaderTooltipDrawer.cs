using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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

        // ヘッダーのテキストと、ヘッダー自体に表示するツールチップを設定
        // ここではフィールド名（変数名）を変数名の代わりにヘッダーとして表示する意図と解釈し、
        // プロパティの表示名を使用します。
        string headerText = label.text; // 変数名（"My Variable"など）をヘッダーとして使用

        // ツールチップ付きのヘッダー用コンテンツを作成
        GUIContent headerContent = new GUIContent(headerText, att.Explanation);

        // ヘッダーを描画するためのRectを計算
        // position.y から開始
        Rect headerRect = new Rect(position.x, position.y, position.width, HEADERHEIGHT);

        // フィールドを描画するためのRectを計算
        // 開始位置: position.y + Headerの高さ + スペース
        // 高さ: プロパティの本来の高さを使用する
        float propertyHeight = EditorGUI.GetPropertyHeight(property, label, true);
        Rect fieldRect = new Rect(position.x, position.y + HEADERHEIGHT + SPACEBELOWHEADER,
                                  position.width, propertyHeight);

        // Unityのシステムに従って描画を開始
        EditorGUI.BeginProperty(position, label, property);

        // --- 1. ヘッダーの描画 (ヘッダー自体にツールチップを持たせる) ---

        // Headerの背景色を少し暗くするなどして見やすくする（オプション）
        // GUI.Box(headerRect, "", EditorStyles.helpBox); 

        // 標準のHeaderAttributeと同様のスタイルで描画（太字など）
        // ヘッダーにツールチップを表示させるため、LabelFieldにTooltip付きのContentを使用
        EditorGUI.LabelField(headerRect, headerContent, EditorStyles.boldLabel);


        // --- 2. フィールド（プロパティ）の描画 (フィールド自体にツールチップを持たせる) ---

        // Tooltipテキストをセット
        // 標準のプロパティ名 (label.text) を使用し、ツールチップとして説明文をセット
        GUIContent fieldContent = new GUIContent(label.text, att.Explanation);

        // 標準のプロパティを描画。これで、リストなど複数行のプロパティも正しく描画されます。
        // propertyHeightを使用することで、配列などが展開された際にも正しく高さを取得します。
        EditorGUI.PropertyField(fieldRect, property, fieldContent, true);

        // 描画を終了
        EditorGUI.EndProperty();
    }
}