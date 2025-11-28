using System.Collections.Generic;
using Game.Script;
using UnityEngine;

public class RecipeService
{
    public readonly Dictionary<(PickupItemType, WorkstationsType), Recipe> Recipes = new();
    private readonly RecipesDB _recipesDB;

    public RecipeService(GameResources gameResources)
    {
        _recipesDB = gameResources.Recipes_DB;
        Initialize();
    }

    public void Initialize()
    {
        foreach (var p in _recipesDB.processors)
        {
            var key = (inputItem: p.inputItemType, workstation: p.workstationType);

            if (p.inputItemType == PickupItemType.None || p.workstationType == WorkstationsType.None ||
                p.outputItemType == PickupItemType.None)
            {
                Debug.LogError($"Recipe {p.name} is incomplete or uses 'None' types!");
                continue;
            }

            if (Recipes.ContainsKey(key))
            {
                Debug.LogError($"Duplicate recipe found for: {key.Item1} + {key.Item2}");
                continue;
            }

            Recipes.Add(key, p);
        }

        Debug.Log($"Loaded {Recipes.Count} recipes.");
    }

    public bool TryGetRecipe(PickupItemType inputItemType, WorkstationsType workstationType, out Recipe processor)
    {
        var key = (inputItem: inputItemType, workstation: workstationType);
        return Recipes.TryGetValue(key, out processor);
    }
}