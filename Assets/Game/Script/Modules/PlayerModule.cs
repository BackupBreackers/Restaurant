using System;
using Leopotam.EcsProto;

class PlayerModule : IProtoModule
{
    public void Init(IProtoSystems systems)
    {
        systems
            .AddSystem(new TimerSystem())
            .AddSystem(new UpdateInputSystem())
            .AddSystem(new PlayerMovementSystem())
            .AddSystem(new HealthSystem())
            .AddSystem(new PlayerTargetSystem());
    }

    public IProtoAspect[] Aspects()
    {
        return new IProtoAspect[] { new PlayerAspect(), new BaseAspect() };
    }

    public Type[] Dependencies()
    {
        //return new[] { typeof(PhysicsModule) };
        return null;
    }
}