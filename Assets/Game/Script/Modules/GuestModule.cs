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
                .AddSystem(new GuestMovementSystem())
                .AddSystem(new GuestWaitingSystem());
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
