using System;
using Game.Script.Factories;
using Leopotam.EcsProto;
using UnityEngine;
using VContainer;

internal class WorkstationsModule : IProtoModule
{
    RefrigeratorSystem _refrigeratorSystem;

    public WorkstationsModule(RefrigeratorSystemFactory refrigeratorSystemFactory)
    {
        _refrigeratorSystem = refrigeratorSystemFactory.CreateProtoSystem();
    }
    
    public void Init(IProtoSystems systems)
    {
        systems
            .AddSystem(_refrigeratorSystem)
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