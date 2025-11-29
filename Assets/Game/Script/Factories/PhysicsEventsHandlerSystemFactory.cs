using UnityEngine;

namespace Game.Script.Factories
{
    public class PhysicsEventsHandlerSystemFactory
    {
        public PhysicsEventsHandlerSystem CreateProtoSystem() => new();
    }
}