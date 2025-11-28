using System;
using Game.Script.Factories;
using Game.Script.Systems;
using Leopotam.EcsProto;

internal class WorkstationsModule : IProtoModule
{
    RefrigeratorSystem _refrigeratorSystem;
    StoveSystem _stoveSystem;

    public WorkstationsModule(RefrigeratorSystemFactory refrigeratorSystemFactory, StoveSystemFactory stoveSystemFactory)
    {
        _refrigeratorSystem = refrigeratorSystemFactory.CreateProtoSystem();
        _stoveSystem = stoveSystemFactory.CreateProtoSystem();
    }
    
    public void Init(IProtoSystems systems)
    {
        systems
            .AddSystem(new PickPlaceSystem())
            .AddSystem(_refrigeratorSystem)
            .AddSystem(new TableInteractionSystem())
            .AddSystem(_stoveSystem)
            .AddSystem(new ClearSystem(), 999);
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