using System;
using Game.Script.Aspects;
using Game.Script.Factories;
using Game.Script.Systems;
using Leopotam.EcsProto;

namespace Game.Script.Modules
{
    public class GuestModule : IProtoModule
    {
        private readonly GroupGenerationSystem _groupGenerationSystem;
        public GuestModule(GroupGenerationSystemFactory groupGenerationSystemFactory)
        {
            this._groupGenerationSystem = groupGenerationSystemFactory.CreateProtoSystem();
        }
        
        public void Init(IProtoSystems systems)
        {
            systems
                .AddSystem(_groupGenerationSystem)
                .AddSystem(new GuestGroupTableResolveSystem())
                .AddSystem(new GuestNavigationSystem())
                .AddSystem(new GuestMovementSystem())
                .AddSystem(new GuestWaitingSystem())
                .AddSystem(new GuestsDestroyerSystem())
                .AddSystem(new EndGameSystem());
        }

        public IProtoAspect[] Aspects()
        {
            return new IProtoAspect[] { new GuestAspect(), new GuestGroupAspect() };
        }

        public Type[] Dependencies()
        {
            return null;
        }
    }
}
