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
        _iterator = new(new[] { typeof(SyncGridPositionEvent) });
        _iterator.Init(_world);
    }

    public void Run()
    {
        foreach (var entity in _iterator)
        {
            var VectorDoesNotExists = new Vector2Int(-9999,-9999);
            ref var syncComponent = ref _placementAspect.SyncMyGridPositionEventPool.Get(entity);
            var entitiesToSync = new Dictionary<Vector2Int,ProtoEntity>();
            if (syncComponent.entityGridPositions.Count == 0) //если компонент из авторинга
            {
                entitiesToSync[VectorDoesNotExists]=entity;
            }
            else //если компонент на игроке
            {
                entitiesToSync = syncComponent.entityGridPositions.Select(e => (e, worldGrid.GetElement(e)))
                    .Where(e => e.Item2.Item2)
                    .Select(e => (e.Item1, e.Item2.Item1.TryUnpack(out var _, out var en), en))
                    .Where(e => e.Item2)
                    .ToDictionary(e => e.Item1, e => e.Item3);
            }

            foreach(var pos in entitiesToSync.Keys)
            {
                var posInGrid = pos;
                if (posInGrid.Equals(VectorDoesNotExists))
                {
                    ref var transformPosition = ref _physicsAspect.PositionPool.Get(entity);
                    posInGrid = new Vector2Int((int)(transformPosition.Position.x / worldGrid.PlacementZoneCellSize.x),
                        (int)(transformPosition.Position.y / worldGrid.PlacementZoneCellSize.y));
                    var packed = _world.PackEntityWithWorld(entity);
                    worldGrid.AddElement(posInGrid, () => packed);
                }
                ref var gridPositionComponent = ref _physicsAspect.GridPositionPool.Get(entitiesToSync[pos]);
                gridPositionComponent.Position = posInGrid;

                if (syncComponent.entityGridPositions.Count == 0)
                {
                    _placementAspect.SyncMyGridPositionEventPool.DelIfExists(entity);
                }
                else
                {
                    syncComponent.entityGridPositions.Remove(pos);
                    if (syncComponent.entityGridPositions.Count == 0)
                        _placementAspect.SyncMyGridPositionEventPool.DelIfExists(entity);
                }
            }
        }
    }

    public void Destroy()
    {
        _iterator = null;
    }
}
