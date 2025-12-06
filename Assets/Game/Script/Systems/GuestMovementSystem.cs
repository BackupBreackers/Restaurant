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
            ref var agent = ref _guestAspect.NavMeshAgentComponentPool.Get(guestEntity).Agent;
            ref var rb = ref _physicsAspect.Rigidbody2DPool.Get(guestEntity);

            if (!agent.pathPending && agent.remainingDistance < 0.3f)
            {
                _guestAspect.ReachedTargetPositionEventPool.Add(guestEntity);
                _guestAspect.GuestIsWalkingTagPool.Del(guestEntity);
                agent.isStopped = true;
                rb.Rigidbody2D.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                agent.isStopped = false;
                rb.Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    public void Destroy()
    {
        _moveIterator = null;
    }
}