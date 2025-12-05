using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto;
using UnityEngine;
using System;

public class CreateGameObjectsSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    [DI] readonly PlacementAspect _placementAspect;

    private PlacementGrid worldGrid;
    private GameResources gameResources;
    private ProtoIt _CreateGOIterator;
    private ProtoWorld _world;

    public CreateGameObjectsSystem(PlacementGrid placementGrid, GameResources gameResources)
    {
        worldGrid = placementGrid;
        this.gameResources = gameResources;
    }

    public void Init(IProtoSystems systems)
    {
        _world = systems.World();
        _CreateGOIterator = new(new[] { typeof(CreateGameObjectEvent) });
        _CreateGOIterator.Init(_world);
    }

    public void Run()
    {
        foreach (var createEvent in _CreateGOIterator)
        {
            ref var component = ref _placementAspect.CreateGameObjectEventPool.Get(createEvent);
            var furn = GetGameObject(component.furnitureType);
            var pivotDiff = new Vector2(0, 0);
            worldGrid.TryGetPivotDifference(component.furnitureType, out pivotDiff);
            var position2D = new Vector2(component.position.x*worldGrid.PlacementZoneCellSize.x,
                component.position.y*worldGrid.PlacementZoneCellSize.y) + worldGrid.PlacementZoneCellSize/2
                + pivotDiff;
            var obj = GameObject.Instantiate(furn,new Vector3(position2D.x,position2D.y,0),Quaternion.identity);
            worldGrid.AddElement(component.position);

            _placementAspect.CreateGameObjectEventPool.DelIfExists(createEvent);
        }
    }

    private GameObject GetGameObject(Type type)
    {
        if (worldGrid.TryGetFurniturePrefab(type, out var furniture)) return furniture;
        throw new NotImplementedException();
    }

    public void Destroy()
    {
        _CreateGOIterator = null;
    }
}
