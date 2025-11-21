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
        // _colExit = new (new[] { typeof(UnityPhysics2DCollisionExitEvent), typeof(Rigidbody2DComponent) });
        _trigEnterIterator = new(new[] { typeof(UnityPhysics2DTriggerEnterEvent) });
        // _trigExit = new (new[] { typeof(UnityPhysics2DTriggerExitEvent), typeof(Rigidbody2DComponent) });

        _colEnterIterator.Init(world);
        // _colExit.Init(world);
        _trigEnterIterator.Init(world);
        // _trigExit.Init(world);
    }

    public void Run()
    {
        //Debug.Log("Run");
        HandleCollisionsEnter();
        // HandleCollisionsExit();
        HandleTriggersEnter();
        // HandleTriggersExit();
    }

    private void HandleCollisionsEnter()
    {
        foreach (var entity in _colEnterIterator)
        {
            ref var evt = ref _unityPhysics2DAspect.CollisionEnterEvent.Get(entity);
        }
    }

    // private void HandleCollisionsExit()
    // {
    //     foreach (var entity in _colExit)
    //     {
    //         ref var evt = ref _events.CollisionExit.Get(entity);
    //         Debug.Log($"[Collision Exit] {evt.Sender.name} — {evt.Other.name}");
    //     }
    // }
    //
    private void HandleTriggersEnter()
    {
        foreach (var entity in _trigEnterIterator)
        {
            ref var evt = ref _unityPhysics2DAspect.TriggerEnterEvent.Get(entity);
            Debug.Log(evt.SenderName);
            // if(evt.Collider.gameObject.tag = "Player")
            // {
            //     ref var new 
            // }
        }
    }

    // private void HandleTriggersExit()
    // {
    //     foreach (var entity in _trigExit)
    //     {
    //         ref var evt = ref _events.TriggerExit.Get(entity);
    //         Debug.Log($"[Trigger Exit] {evt.Sender.name} exited {evt.Other.name}");
    //     }
    // }
}