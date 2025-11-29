using Leopotam.EcsProto.Unity.Editor;
using UnityEditor;

sealed class ItemSourceComponentInspector : ProtoComponentInspector<ItemSourceComponent>
{
    protected override bool OnRender(string label, ref ItemSourceComponent value)
    {
        EditorGUILayout.LabelField($"Super ItemSourceComponent component", EditorStyles.boldLabel);
        string newValue = EditorGUILayout.TextField("Name", value.Name);
        EditorGUILayout.HelpBox($"Hello, {value.Name}", MessageType.Info);
        // Если значение не поменялось - возвращаем false.
        if (newValue == value.Name)
        {
            return false;
        }

        // Иначе - обновляем значение и возвращаем true.
        value.Name = newValue;
        return true;
    }
}