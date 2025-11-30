using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

public class GuestMovementSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    [DI] private readonly GuestAspect _guestAspect = default;
    [DI] private readonly PhysicsAspect _physics = default;
    private ProtoWorld _world = default;
    private Vector2 _guestDeathPlace = new Vector2(0, -10); //  #TODO DI+rename

    private ProtoIt _tablesIteartor;
    private ProtoIt _moveIterator;
    private ProtoIt _startLeavingIterator;
    
    public void Init(IProtoSystems systems)
    {
        _world = systems.World();

        _tablesIteartor = new(new[]
        {
            typeof(GuestTable)
        });
        _moveIterator = new(new[]
        {
            typeof(GuestIsWalking), typeof(GuestTag)
        });
        _startLeavingIterator = new(new[]
        {
            typeof(GuestLeavingEvent)
        });
        _moveIterator.Init(_world);
        _startLeavingIterator.Init(_world);
        _tablesIteartor.Init(_world);
    }

    public void Run()
    {
        foreach (var entity in _startLeavingIterator)
        {
            if (_guestAspect.TargetPositionComponentPool.Has(entity) &&
                !_guestAspect.DidGotOrderPool.Has(entity)) break;
            if (!_guestAspect.TargetPositionComponentPool.Has(entity))
            {
                ref var newTarget = ref _guestAspect.TargetPositionComponentPool.Add(entity);
                newTarget.Position = _guestDeathPlace;
            }
            else
            {
                ref var newTarget = ref _guestAspect.TargetPositionComponentPool.Get(entity);
                newTarget.Position = _guestDeathPlace;
            }
            ref Rigidbody2DComponent rb = ref _physics.Rigidbody2DPool.Get(entity);
            rb.Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _guestAspect.GuestLeavingEventPool.DelIfExists(entity);
        }
        foreach (var entity in _moveIterator)
        {
            foreach (var tableEntity in _tablesIteartor)
            {
                if (_guestAspect.TargetPositionComponentPool.Has(entity)
                    || _guestAspect.GuestLeavingEventPool.Has(tableEntity))
                    break;
                var targetTable = _guestAspect.GuestTablePool.Get(tableEntity);
                var newTarget = targetTable.guestPlaces[0];
                ref var oldTarget = ref _guestAspect.TargetPositionComponentPool.GetOrAdd(entity);
                oldTarget.Position = newTarget;
                oldTarget.Table = _world.PackEntityWithWorld(tableEntity);
            }
            ref var target = ref _guestAspect.TargetPositionComponentPool.Get(entity);
            ref PositionComponent position = ref _guestAspect.PositionComponentPool.Get(entity);
            ref Rigidbody2DComponent rb = ref _physics.Rigidbody2DPool.Get(entity);
            if (Vector2.Distance(position.Position, target.Position) < 0.5f)
            {
                rb.Rigidbody2D.linearVelocity = Vector2.zero;
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
