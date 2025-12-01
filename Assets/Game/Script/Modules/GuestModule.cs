using System;
using Game.Script.Aspects;
using Game.Script.Systems;
using Leopotam.EcsProto;

namespace Game.Script.Modules
{
    public class GuestModule : IProtoModule
    {
        public void Init(IProtoSystems systems)
        {
            systems
                .AddSystem(new GuestNavigationSystem())
                .AddSystem(new GuestMovementSystem())
                .AddSystem(new GuestWaitingSystem())
                .AddSystem(new GuestsDestroyerSystem())
                .AddSystem(new EndGameSystem());
            //.AddSystem(new GuestEatSystem());
        }

        public IProtoAspect[] Aspects()
        {
            return new IProtoAspect[] { new GuestAspect() };
        }

        public Type[] Dependencies()
        {
            return null;
        }
    }
}
