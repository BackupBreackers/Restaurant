using System;
using Leopotam.EcsProto;

class PlayerModule : IProtoModule
{
    public void Init(IProtoSystems systems)
    {
        systems
            .AddSystem(new UpdateInputSystem())
            .AddSystem(new PlayerMovementSystem())
            .AddSystem(new HealthSystem());
    }

    public IProtoAspect[] Aspects()
    {
        return new IProtoAspect[] { new PlayerAspect(), new PhysicsAspect() };
    }

    public Type[] Dependencies()
    {
        return new[] { typeof(PhysicsModule) };
    }
}