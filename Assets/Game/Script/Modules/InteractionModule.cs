using System;
using Leopotam.EcsProto;

internal class InteractionModule : IProtoModule
{
    public void Init(IProtoSystems systems)
    {
        systems
            .AddSystem(new InteractionSystem());
        //.AddSystem(new TargetingSystem());
    }

    public IProtoAspect[] Aspects()
    {
        return new IProtoAspect[] { new InteractionAspect() };
    }

    public Type[] Dependencies()
    {
        return null;
    }
}