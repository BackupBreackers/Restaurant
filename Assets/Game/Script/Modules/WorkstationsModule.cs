using System;
using Game.Script.Factories;
using Game.Script.Systems;
using Leopotam.EcsProto;
using UnityEngine;

internal class WorkstationsModule : IProtoModule
{
    RefrigeratorSystem _refrigeratorSystem;
    StoveSystem _stoveSystem;
    PickPlaceSystem _pickPlaceSystem;
    TableInteractionSystem _tableInteractionSystem;
    ClearSystem _clearSystem;

    public WorkstationsModule(RefrigeratorSystemFactory refrigeratorSystemFactory,
        StoveSystemFactory stoveSystemFactory,
        PickPlaceSystemFactory pickPlaceSystemFactory,
        TableInteractionSystemFactory tableInteractionSystemFactory,
        ClearSystemFactory clearSystemFactory
        )
    {
        _refrigeratorSystem = refrigeratorSystemFactory.CreateProtoSystem();
        _stoveSystem = stoveSystemFactory.CreateProtoSystem();
        _pickPlaceSystem = pickPlaceSystemFactory.CreateProtoSystem();
        _tableInteractionSystem = tableInteractionSystemFactory.CreateProtoSystem();
        _clearSystem = clearSystemFactory.CreateProtoSystem();
    }
    
    public void Init(IProtoSystems systems)
    {
        systems
            .AddSystem(_pickPlaceSystem)
            .AddSystem(new GuestTableSetupSystem())
            .AddSystem(new TableNotificationSystem())
            .AddSystem(_refrigeratorSystem)
            .AddSystem(_tableInteractionSystem)
            .AddSystem(_stoveSystem)
            .AddSystem(new ProgressBarSystem())
            .AddSystem(_clearSystem, 999);
    }

    public IProtoAspect[] Aspects()
    {
        return new IProtoAspect[] { new WorkstationsAspect(), new ItemAspect() };
    }

    public Type[] Dependencies()
    {
        return null;
    }
}