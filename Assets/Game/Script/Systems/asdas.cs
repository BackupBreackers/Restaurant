#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using Game.Script;

[CustomPropertyDrawer(typeof(SerializableType))]
public class SerializableTypeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty typeNameProp = property.FindPropertyRelative("typeName");

        Type currentType = string.IsNullOrEmpty(typeNameProp.stringValue) ? null : Type.GetType(typeNameProp.stringValue);

        // Получаем все конкретные наследники PickableItem
        List<Type> subclasses = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(PickableItem)) && !t.IsAbstract)
            .ToList();

        // Построим массив для popup
        string[] typeNames = subclasses.Select(t => t.Name).ToArray();
        int currentIndex = currentType != null ? subclasses.IndexOf(currentType) : -1;

        int newIndex = EditorGUI.Popup(position, label.text, currentIndex, typeNames);

        if (newIndex >= 0 && newIndex < subclasses.Count)
        {
            typeNameProp.stringValue = subclasses[newIndex].AssemblyQualifiedName;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
#endif
[Serializable]
public class SerializableType
{
    [SerializeField]
    private string typeName; // AssemblyQualifiedName

    public Type Type
    {
        get => string.IsNullOrEmpty(typeName) ? null : Type.GetType(typeName);
        set => typeName = value?.AssemblyQualifiedName;
    }
}