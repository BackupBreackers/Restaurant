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
    private Dictionary<Vector2Int, Func<ProtoPackedEntityWithWorld>> worldGrid = new();

    public PlacementGrid()
    {
        var grid = GameObject.FindAnyObjectByType<Grid>();
        PlacementZoneCellSize = new Vector2(grid.cellSize.x,grid.cellSize.y);
        PlacementZoneWorldStart = new Vector2(PlacementZoneIndexStart.x * PlacementZoneCellSize.x,
            PlacementZoneIndexStart.y * PlacementZoneCellSize.y);
    }

    public bool IsContains(Vector2Int v) => worldGrid.ContainsKey(v);

    public void DeleteElement(Vector2Int v)
    {
        if (worldGrid.ContainsKey(v))
        {
            worldGrid.Remove(v);
        }
    }

    public void AddElement(Vector2Int v, Func<ProtoPackedEntityWithWorld> entityGetter) =>
        worldGrid.Add(v, entityGetter);

    public (ProtoPackedEntityWithWorld, bool) GetElement(Vector2Int v) 
    {
        if (worldGrid.ContainsKey(v))
            return (worldGrid[v](), true);
        return (default, false);
    }

    public void SwitchElement(Vector2Int lastPos, Vector2Int newPos)
    {
        worldGrid.Add(newPos, worldGrid[lastPos]);
        DeleteElement(lastPos);
    }
}
