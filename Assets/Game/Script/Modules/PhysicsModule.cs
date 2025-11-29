using System;
using Game.Script.Factories;
using Leopotam.EcsProto;
using UnityEngine;

internal class PhysicsModule : IProtoModule
{
    private PhysicsEventsHandlerSystem _physicsEventsHandlerSystem;
    private SyncUnityPhysicsToEcsSystem _syncUnityPhysicsToEcsSystem;
    public PhysicsModule(PhysicsEventsHandlerSystemFactory physicsEventsHandlerSystemFactory,
        SyncUnityPhysicsToEcsSystemFactory syncUnityPhysicsToEcsSystemFactory)
    {
        _physicsEventsHandlerSystem = physicsEventsHandlerSystemFactory.CreateProtoSystem();
        _syncUnityPhysicsToEcsSystem = syncUnityPhysicsToEcsSystemFactory.CreateProtoSystem();
    }
    
    public void Init(IProtoSystems systems)
    {
        systems
            .AddSystem(_physicsEventsHandlerSystem)
            .AddSystem(_syncUnityPhysicsToEcsSystem);
            //.AddSystem(new UpdateItemViewPosition());
    }

    public IProtoAspect[] Aspects()
    {
        return new IProtoAspect[] { new PhysicsAspect()};
    }

    public Type[] Dependencies()
    {
        return null;
    }
}