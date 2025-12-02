using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto;
using UnityEngine;
using System;

public class CreateGameObjectsSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    [DI] readonly PlacementAspect _placementAspect;

    private PlacementGrid worldGrid;
    private GameResources gameResources;
    private ProtoIt _iterator;
    private ProtoWorld _world;

    public CreateGameObjectsSystem(PlacementGrid placementGrid, GameResources gameResources)
    {
        worldGrid = placementGrid;
        this.gameResources = gameResources;
    }

    public void Init(IProtoSystems systems)
    {
        _world = systems.World();
        _iterator = new(new[] { typeof(CreateGameObjectEvent) });
        _iterator.Init(_world);
    }

    public void Run()
    {
        foreach (var entity in _iterator)
        {
            ref var component = ref _placementAspect.CreateGameObjectEventPool.Get(entity);
            var furn = GetGameObject(component.furnitureType);
            var position2D = new Vector2(component.position.x*worldGrid.PlacementZoneCellSize.x,
                component.position.y*worldGrid.PlacementZoneCellSize.y) + worldGrid.PlacementZoneCellSize/2;
            var obj = GameObject.Instantiate(furn,new Vector3(position2D.x,position2D.y,0),Quaternion.identity);
            worldGrid.AddElement(component.position, obj.GetComponent<MyAuthoring>().Entity);

            if (!_placementAspect.SyncMyGridPositionEventPool.Has(entity))
                _placementAspect.SyncMyGridPositionEventPool.Add(entity);
            ref var sync = ref _placementAspect.SyncMyGridPositionEventPool.Get(entity);
            if (sync.entityGridPositions is null)
                sync.entityGridPositions = new();
            sync.entityGridPositions.Add(component.position);

            _placementAspect.CreateGameObjectEventPool.DelIfExists(entity);
        }
    }

    private GameObject GetGameObject(FurnitureType type)
    {
        switch (type)
        {
            case FurnitureType.Fridge:
                return gameResources.Fridge.gameObject;
        }
        throw new NotImplementedException("Остальные типы мебели пока не добавлены");
    }

    public void Destroy()
    {
        _iterator = null;
    }
}
