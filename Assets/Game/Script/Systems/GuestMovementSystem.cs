using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

public class GuestMovementSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    [DI] private readonly GuestAspect _guestAspect;
    [DI] private readonly PhysicsAspect _physicsAspect;

    private ProtoWorld _world;

    private ProtoIt _moveIterator;

    public void Init(IProtoSystems systems)
    {
        _world = systems.World();

        _moveIterator = new(new[]
        {
            typeof(GuestTag), typeof(PositionComponent), typeof(GuestIsWalkingTag),
            typeof(TargetPositionComponent), typeof(MovementSpeedComponent), typeof(Rigidbody2DComponent),
        });
        _moveIterator.Init(_world);
    }

    public void Run()
    {
        foreach (var guestEntity in _moveIterator)
        {
            ref var guestPosition = ref _physicsAspect.PositionPool.Get(guestEntity).Position;
            ref var targetPosition = ref _guestAspect.TargetPositionComponentPool.Get(guestEntity).Position;
            ref var rb = ref _physicsAspect.Rigidbody2DPool.Get(guestEntity);
            

            if (Vector2.Distance(guestPosition, targetPosition) < 0.5f)
            {
                _guestAspect.ReachedTargetPositionEventPool.Add(guestEntity);
                _guestAspect.GuestIsWalkingTagPool.Del(guestEntity);
                
                rb.Rigidbody2D.bodyType = RigidbodyType2D.Static;
                continue;
            }

            ref var movementSpeed = ref _physicsAspect.MovementSpeedPool.Get(guestEntity);

            var direction = (targetPosition - guestPosition).normalized;
            rb.Rigidbody2D.linearVelocity = direction * movementSpeed.Value;
        }
    }

    public void Destroy()
    {
        _moveIterator = null;
    }
}