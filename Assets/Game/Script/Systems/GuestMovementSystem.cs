using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Ai.Utility;
using Leopotam.EcsProto.QoL;
using UnityEngine;

public class GuestMovementSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    [DI] readonly AiUtilityAspect _aiUtilityAspect = default;
    [DI] readonly ProtoIt _responses = new (It.Inc<AiUtilityResponseEvent> ());
    private GuestAspect _guestAspect;
    private PhysicsAspect _physicsAspect;
    
    private ProtoIt _iterator;
    
    public void Init(IProtoSystems systems)
    {
        var world = systems.World();
        _guestAspect = (GuestAspect)world.Aspect(typeof(GuestAspect));
        _physicsAspect = (PhysicsAspect)world.Aspect(typeof(PhysicsAspect));
        
        _iterator = new(new[]
        {
            typeof(MovementSpeedComponent), typeof(PositionComponent),
            typeof(TargetPositionComponent),
        });
        _iterator.Init(world);
    }

    public void Run()
    {
        foreach (var entity in _iterator)
            _aiUtilityAspect.Request(entity);

        foreach (var entity in _responses)
        {
            ref AiUtilityResponseEvent result = ref _aiUtilityAspect.ResponseEvent.Get(entity);
            if (result.Solver is null)
            {
                ref Rigidbody2DComponent rb = ref _physicsAspect.Rigidbody2DPool.Get(entity);
                rb.Rigidbody2D.linearVelocity = Vector2.zero;
                return;
            }
            result.Solver.Apply(entity);
        }
    }

    public void Destroy()
    {
        _guestAspect = null;
        _physicsAspect = null;
        _iterator = null;
    }
}
