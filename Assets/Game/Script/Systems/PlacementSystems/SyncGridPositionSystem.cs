using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SyncGridPositionSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    [DI] readonly PlacementAspect _placementAspect;
    [DI] readonly PhysicsAspect _physicsAspect;

    private PlacementGrid worldGrid;
    private ProtoIt _iterator;
    private ProtoWorld _world;

    public SyncGridPositionSystem(PlacementGrid placementGrid) =>
        worldGrid = placementGrid;

    public void Init(IProtoSystems systems)
    {
        _world = systems.World();
        _iterator = new(new[] { typeof(SyncGridPositionEvent), typeof(PositionComponent) });
        _iterator.Init(_world);
    }

    public void Run()
    {
        foreach (var entity in _iterator)
        {
            ref var syncComponent = ref _placementAspect.SyncMyGridPositionEventPool.Get(entity);
            ref var transformPosition = ref _physicsAspect.PositionPool.Get(entity);
            var posInGrid = new Vector2Int((int)(transformPosition.Position.x / worldGrid.PlacementZoneCellSize.x),
                (int)(transformPosition.Position.y / worldGrid.PlacementZoneCellSize.y));
            worldGrid.AddElement(posInGrid);
            ref var gridPositionComponent = ref _physicsAspect.GridPositionPool.Get(entity);
            gridPositionComponent.Position = posInGrid;

            _placementAspect.SyncMyGridPositionEventPool.DelIfExists(entity);
        }
    }

    public void Destroy()
    {
        _iterator = null;
    }
}
