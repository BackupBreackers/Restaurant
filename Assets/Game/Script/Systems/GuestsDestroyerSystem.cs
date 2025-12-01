using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

namespace Game.Script.Systems
{
    public class GuestsDestroyerSystem : IProtoInitSystem, IProtoRunSystem
    {
        [DI] private GuestAspect _guestAspect;
        private ProtoWorld _world;
        private ProtoIt _deadGuests;

        public void Init(IProtoSystems systems)
        {
            _world = systems.World();
            _deadGuests = new(new[] { typeof(GuestServicedTag), typeof(ReachedTargetPositionEvent) });
            _deadGuests.Init(_world);
        }

        public void Run()
        {
            foreach (var guest in _deadGuests)
            {
                Debug.Log("кремируйте её быстрее");
                _world.DelEntity(guest);
            }
        }
    }
}