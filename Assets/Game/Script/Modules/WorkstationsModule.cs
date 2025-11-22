using System;
using Leopotam.EcsProto;

internal class WorkstationsModule : IProtoModule
{
    public void Init(IProtoSystems systems)
    {
        systems
            .AddSystem(new RefrigeratorSystem())
            .AddSystem(new TableInteractionSystem());
    }

    public IProtoAspect[] Aspects()
    {
        return new IProtoAspect[] { new WorkstationsAspect() };
    }

    public Type[] Dependencies()
    {
        return null;
    }
}