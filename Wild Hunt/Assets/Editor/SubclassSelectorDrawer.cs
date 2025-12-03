#if UNITY_2019_3_OR_NEWER

using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
public class SubclassSelectorDrawer : PropertyDrawer
{
    bool initialized = false;
    Type[] inheritedTypes;
    string[] typePopupNameArray;
    string[] typeFullNameArray;
    int currentTypeIndex;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.ManagedReference) return;
        if (!initialized)
        {
            Initialize(property);
            GetCurrentTypeIndex(property.managedReferenceFullTypename);
            initialized = true;
        }
        int selectedTypeIndex = EditorGUI.Popup(GetPopupPosition(position), currentTypeIndex, typePopupNameArray);
        UpdatePropertyToSelectedTypeIndex(property, selectedTypeIndex);
        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }

    private void Initialize(SerializedProperty property)
    {
        SubclassSelectorAttribute utility = (SubclassSelectorAttribute)attribute;
        GetAllInheritedTypes(GetType(property), utility.IsIncludeMono());
        GetInheritedTypeNameArrays();
    }

    private void GetCurrentTypeIndex(string typeFullName)
    {
        currentTypeIndex = Array.IndexOf(typeFullNameArray, typeFullName);
    }
    /// <summary>
    /// 全ての継承されたタイプを取得
    /// </summary>
    /// <param name="baseType"></param>
    /// <param name="includeMono"></param>
    void GetAllInheritedTypes(Type baseType, bool includeMono)
    {
        Type monoType = typeof(MonoBehaviour);
        inheritedTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => baseType.IsAssignableFrom(p) && p.IsClass && (!monoType.IsAssignableFrom(p) || includeMono))
            .Prepend(null)
            .ToArray();
    }

    private void GetInheritedTypeNameArrays()
    {
        typePopupNameArray = inheritedTypes.Select(type => type == null ? "<null>" : type.ToString()).ToArray();
        typeFullNameArray = inheritedTypes.Select(type => type == null ? "" : string.Format("{0} {1}", type.Assembly.ToString().Split(',')[0], type.FullName)).ToArray();
    }

    public void UpdatePropertyToSelectedTypeIndex(SerializedProperty property, int selectedTypeIndex)
    {
        if (currentTypeIndex == selectedTypeIndex) return;
        currentTypeIndex = selectedTypeIndex;
        Type selectedType = inheritedTypes[selectedTypeIndex];
        property.managedReferenceValue =
            selectedType == null ? null : Activator.CreateInstance(selectedType);
    }

    Rect GetPopupPosition(Rect currentPosition)
    {
        Rect popupPosition = new Rect(currentPosition);
        popupPosition.width -= EditorGUIUtility.labelWidth;
        popupPosition.x += EditorGUIUtility.labelWidth;
        popupPosition.height = EditorGUIUtility.singleLineHeight;
        return popupPosition;
    }

    public static Type GetType(SerializedProperty property)
    {
        const BindingFlags bindingAttr =
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.FlattenHierarchy |
                BindingFlags.Instance
            ;

        var propertyPaths = property.propertyPath.Split('.');
        Type currentType = property.serializedObject.targetObject.GetType();

        for (int i = 0; i < propertyPaths.Length; i++)
        {
            string path = propertyPaths[i];

            // 配列・リストのインデックスアクセス "Array.data[0]" をスキップ
            if (path == "Array")
            {
                i++; // data[x] もスキップ
                continue;
            }

            FieldInfo fieldInfo = currentType.GetField(path, bindingAttr);
            if (fieldInfo == null) break;

            Type fieldType = fieldInfo.FieldType;

            // 配列の場合
            if (fieldType.IsArray)
            {
                currentType = fieldType.GetElementType();
            }
            // リストの場合
            else if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.List<>))
            {
                currentType = fieldType.GetGenericArguments()[0];
            }
            else
            {
                currentType = fieldType;
            }
        }

        return currentType;
    }
}
#endif