using System;
using Game.Script.Aspects;
using Leopotam.EcsProto;

namespace Game.Script.Modules
{
    public class GuestModule : IProtoModule
    {
        public void Init(IProtoSystems systems)
        {
            systems
                .AddSystem(new GuestMovementSystem());
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
