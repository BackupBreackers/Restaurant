#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Linq;

public static class PickableItemSOGenerator
{
    private const string SavePath = "Assets/Game/Resources/PickableItems";

    [MenuItem("Tools/Generate PickableItem SOs")]
    public static void GeneratePickableItemSOs()
    {
        if (!Directory.Exists(SavePath))
            Directory.CreateDirectory(SavePath);

        // Находим все наследники PickableItem
        var pickableTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(asm => asm.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(Game.Script.PickableItem)) && !t.IsAbstract)
            .ToList();

        foreach (var type in pickableTypes)
        {
            string assetName = type.Name + ".asset";
            string assetPath = Path.Combine(SavePath, assetName);

            if (File.Exists(assetPath))
                continue;

            // Создаем пустой SO конкретного типа
            var so = ScriptableObject.CreateInstance(type) as Game.Script.PickableItem;
            AssetDatabase.CreateAsset(so, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("PickableItem SO generation complete!");
    }
}

public static class PickableItemSOChecker
{
    private const string SavePath = "Assets/Game/Resources/PickableItems";

    [MenuItem("Tools/Check PickableItem Sprites")]
    public static void CheckPickableItemSprites()
    {
        if (!Directory.Exists(SavePath))
        {
            Debug.LogWarning("Папка с PickableItems не найдена: " + SavePath);
            return;
        }

        string[] guids = AssetDatabase.FindAssets("", new[] { SavePath });

        int total = guids.Length;
        int missingSprites = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var item = AssetDatabase.LoadAssetAtPath<Game.Script.PickableItem>(path);

            if (item != null && item.PickupItemSprite == null)
            {
                Debug.LogWarning($"У PickableItem '{item.name}' не установлен спрайт!", item);
                missingSprites++;
            }
        }

        if (missingSprites == 0)
            Debug.Log($"Все {total} PickableItem имеют спрайт!");
        else
            Debug.Log($"Проверка завершена. Всего PickableItem: {total}, без спрайта: {missingSprites}");
    }
}
#endif