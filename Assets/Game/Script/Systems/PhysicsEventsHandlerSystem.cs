using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Physics2D;
using UnityEngine;

public class PhysicsEventsHandlerSystem : IProtoInitSystem, IProtoRunSystem
{
    private PhysicsAspect _physics;
    private UnityPhysics2DAspect _unityPhysics2DAspect;

    private ProtoIt _colEnterIterator;

    // private ProtoIt _colExit;
    private ProtoIt _trigEnterIterator;
    // private ProtoIt _trigExit;

    public void Init(IProtoSystems systems)
    {
        ProtoWorld world = systems.World();
        _unityPhysics2DAspect = (UnityPhysics2DAspect)world.Aspect(typeof(UnityPhysics2DAspect));

        _colEnterIterator = new(new[] { typeof(UnityPhysics2DCollisionEnterEvent) });
        _trigEnterIterator = new(new[] { typeof(UnityPhysics2DTriggerEnterEvent) });

        _colEnterIterator.Init(world);
        _trigEnterIterator.Init(world);
    }

    public void Run()
    {
        HandleCollisionsEnter();
        HandleTriggersEnter();
    }

    private void HandleCollisionsEnter()
    {
        foreach (var entity in _colEnterIterator)
        {
            ref var evt = ref _unityPhysics2DAspect.CollisionEnterEvent.Get(entity);
        }
    }

    private void HandleTriggersEnter()
    {
        foreach (var entity in _trigEnterIterator)
        {
            ref var evt = ref _unityPhysics2DAspect.TriggerEnterEvent.Get(entity);
            Debug.Log(evt.SenderName);
        }
    }
}