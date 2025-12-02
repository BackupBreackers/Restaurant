using System.Collections.Generic;
using System.Linq;
using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

namespace Game.Script.Systems
{
    public class AcceptOrderSystem : IProtoInitSystem, IProtoRunSystem
    {
        [DI] private WorkstationsAspect _workstationsAspect;
        [DI] private GuestAspect _guestAspect;
        private ProtoWorld _world;
        
        private ProtoItExc _interactedTableIterator;
        private ProtoItExc _interactedGuestIterator;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.World();
            _interactedTableIterator = new(new[]
            {
                typeof(GuestTableComponent), typeof(PickPlaceEvent),
                typeof(PositionComponent), typeof(InteractableComponent)
            },
            new[]
            {
                typeof(ItemPickEvent), typeof(ItemPlaceEvent)
            });
            _interactedGuestIterator = new(new[]
            {
                typeof(GuestTag), typeof(WaitingTakeOrderTag)
            }, new[] { typeof(WaitingOrderTag) });
            _interactedTableIterator.Init(_world);
            _interactedGuestIterator.Init(_world);
        }

        public void Run()
        {
            foreach (var tableEntity in _interactedTableIterator)
            {
                ref var tableComponent = ref _workstationsAspect.GuestTablePool.Get(tableEntity);
                
                if (tableComponent.Guests is null || tableComponent.Guests.Count == 0) 
                    continue;

                var unpackedGuests = new HashSet<ProtoEntity>();
                foreach (var packedGuest in tableComponent.Guests)
                    if (packedGuest.TryUnpack(out _, out var guestEntity))
                        unpackedGuests.Add(guestEntity);
                
                foreach (var guestEntity in _interactedGuestIterator)
                {
                    if (!unpackedGuests.Contains(guestEntity))
                        continue;
                    Debug.Log("Получен заказ от гостя");
                    _workstationsAspect.InteractedEventPool.Add(guestEntity);
                }
            }
        }
    }
}