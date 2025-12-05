using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

public class PlacementGrid
{
    public Vector2Int PlacementZoneSize { get; } = new Vector2Int(14, 8);
    public Vector2 PlacementZoneWorldStart { get; private set; }
    public Vector2Int PlacementZoneIndexStart { get; } = new Vector2Int(-7, -4);
    public Vector2 PlacementZoneCellSize { get; private set; }
    private HashSet<Vector2Int> worldGrid = new();
    private GameResources gameResources;
    private Dictionary<Type, GameObject> workstationItems;
    private Dictionary<Type, Vector2> pivotDifferences;

    public PlacementGrid(Grid grid, GameResources gameResources)
    {
        PlacementZoneCellSize = new Vector2(grid.cellSize.x, grid.cellSize.y);
        PlacementZoneWorldStart = new Vector2(PlacementZoneIndexStart.x * PlacementZoneCellSize.x,
            PlacementZoneIndexStart.y * PlacementZoneCellSize.y);
        this.gameResources = gameResources;
        workstationItems = GetWorkstationDict();
        pivotDifferences = GetPivotDifferenceDict();
    }

    public bool IsContains(Vector2Int v) => worldGrid.Contains(v);

    public void DeleteElement(Vector2Int v)
    {
        if (worldGrid.Contains(v))
        {
            worldGrid.Remove(v);
        }
    }

    public void AddElement(Vector2Int v) =>
        worldGrid.Add(v);

    public void SwitchElement(Vector2Int lastPos, Vector2Int newPos)
    {
        AddElement(newPos);
        DeleteElement(lastPos);

        Debug.Log(string.Join(" ", worldGrid));
    }

    public bool TryGetFurniturePrefab(Type type, out GameObject prefab)
    {
        if (workstationItems.ContainsKey(type))
        {
            prefab = workstationItems[type];
            return true;
        }
        prefab = null;
        Debug.LogError($"Проверьте PlacementObjects_DB. Такого типа мебели в словаре нет: {type}");
        return false;
    }

    public bool TryGetPivotDifference(Type type, out Vector2 pivotDifference) 
    {
        if (pivotDifferences.ContainsKey(type))
        {
            pivotDifference = pivotDifferences[type];
            return true;
        }
        pivotDifference = default;
        Debug.LogError($"Проверьте PivotToRealPositionDifferences. Такого типа мебели в словаре нет: {type}");
        return false;
    }

    private Dictionary<Type, GameObject> GetWorkstationDict()
    {
        Dictionary<Type, GameObject> res = new();
        foreach (var placementObject in gameResources.PlacementObjects_DB.furnitures)
        {
            var type = placementObject.workstationType.GetType();
            if (type is null)
            {
                Debug.LogError("В списке попался workstationType null");
                continue;
            }
            if (res.ContainsKey(type))
            {
                Debug.LogError($"Мебель этого типа встретилась дважды: {type}");
                continue;
            }
            res[type] = placementObject.prefab;
        }
        return res;
    }

    private Dictionary<Type, Vector2> GetPivotDifferenceDict()
    {
        Dictionary<Type, Vector2> res = new();
        foreach (var diff in gameResources.PivotToRealPositionDifferences.differenceList)
        {
            var type = diff.item.GetType();
            if (type is null)
            {
                Debug.LogError("В списке попался workstationType null");
                continue;
            }
            if (res.ContainsKey(type))
            {
                Debug.LogError($"Мебель этого типа встретилась дважды: {type}");
                continue;
            }
            res[type] = diff.pivotToRealPositionDifference;
        }
        return res;
    }
}
