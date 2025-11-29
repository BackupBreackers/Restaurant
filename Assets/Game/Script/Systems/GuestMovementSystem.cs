using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

public class GuestMovementSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    [DI] private readonly GuestAspect _guestAspect = default;
    [DI] private readonly PhysicsAspect _physics = default;
    private Vector2 _guestDeathPlace = new Vector2(0, -10); //  #TODO DI+rename
    
    private ProtoIt _moveIterator;
    private ProtoIt _startLeavingIterator;
    
    public void Init(IProtoSystems systems)
    {
        var world = systems.World();
        
        _moveIterator = new(new[]
        {
            typeof(TargetPositionComponent), typeof(GuestTag)
        });
        _startLeavingIterator = new(new[]
        {
            typeof(GuestLeavingEvent)
        });
        _moveIterator.Init(world);
        _startLeavingIterator.Init(world);
    }

    public void Run()
    {
        foreach (var entity in _startLeavingIterator)
        {
            ref var newTarget = ref _guestAspect.TargetPositionComponentPool.Add(entity);
            newTarget.Position = _guestDeathPlace;
            ref Rigidbody2DComponent rb = ref _physics.Rigidbody2DPool.Get(entity);
            rb.Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _guestAspect.GuestLeavingEventPool.DelIfExists(entity);
        }
        foreach (var entity in _moveIterator)
        {
            ref TargetPositionComponent target = ref _guestAspect.TargetPositionComponentPool.Get(entity);
            ref PositionComponent position = ref _guestAspect.PositionComponentPool.Get(entity);
            ref Rigidbody2DComponent rb = ref _physics.Rigidbody2DPool.Get(entity);
            if (Vector2.Distance(position.Position, target.Position) < 0.5f)
            {
                rb.Rigidbody2D.linearVelocity = Vector2.zero;
                _guestAspect.TargetPositionComponentPool.DelIfExists(entity);
                _guestAspect.GuestArrivedEventPool.Add(entity);
                rb.Rigidbody2D.bodyType = RigidbodyType2D.Static;
                continue;
            }
            ref MovementSpeedComponent movementSpeed = ref _guestAspect.MovementSpeedComponentPool.Get(entity);
            var direction = (target.Position - position.Position).normalized;
            rb.Rigidbody2D.linearVelocity = direction * movementSpeed.Value;
        }
    }

    public void Destroy()
    {
        _moveIterator = null;
        _startLeavingIterator = null;
    }
}
