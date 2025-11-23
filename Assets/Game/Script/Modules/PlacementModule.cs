using Leopotam.EcsProto;
using System;
using UnityEngine;

public class PlacementModule : IProtoModule
{
    public void Init(IProtoSystems systems)
    {
        systems
            .AddSystem(new RandomSpawnFurnitureSystem());
    }

    public IProtoAspect[] Aspects()
    {
        return new IProtoAspect[] { new PlacementAspect() };
    }

    public Type[] Dependencies()
    {
        return null;
    }
}
