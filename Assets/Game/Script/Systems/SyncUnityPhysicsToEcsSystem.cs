using Leopotam.EcsProto;
using UnityEngine;

public class SyncUnityPhysicsToEcsSystem : IProtoInitSystem, IProtoRunSystem
{
    private PhysicsAspect _physics;
    private ProtoIt _iterator;

    public void Init(IProtoSystems systems)
    {
        var world = systems.World();
        _physics = (PhysicsAspect)world.Aspect(typeof(PhysicsAspect));

        _iterator = new(new[] { typeof(Rigidbody2DComponent), typeof(PositionComponent) });
        _iterator.Init(world);
    }

    public void Run()
    {
        foreach (var entity in _iterator)
        {
            ref var rb = ref _physics.Rigidbody2DPool.Get(entity);
            if (!rb.Rigidbody2D)
                continue;
            ref var pos = ref _physics.PositionPool.Get(entity);
            
            pos.Position = rb.Rigidbody2D.position;
        }
    }
}