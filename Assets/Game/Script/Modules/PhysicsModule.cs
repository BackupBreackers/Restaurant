using System;
using Leopotam.EcsProto;

internal class PhysicsModule : IProtoModule
{
    public void Init(IProtoSystems systems)
    {
        systems
            .AddSystem(new PhysicsEventsHandlerSystem())
            .AddSystem(new SyncUnityPhysicsToEcsSystem())
            .AddSystem(new UpdateItemViewPosition());
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