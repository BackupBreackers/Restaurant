using System.Collections.Generic;
using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

namespace Game.Script.Systems
{
    public class TableNotificationSystem : IProtoInitSystem, IProtoRunSystem
    {
        [DI] private WorkstationsAspect _workstationsAspect;
        [DI] private PlayerAspect _playerAspect;
        [DI] private GuestAspect _guestAspect;
        [DI] private BaseAspect _baseAspect;
        private ProtoIt _tablesIterator;

        public void Init(IProtoSystems systems)
        {
            var world = systems.World();
            _tablesIterator = new(new[]
            {
                typeof(GuestTableComponent), typeof(ItemPlaceEvent), typeof(HolderComponent),
                typeof(PositionComponent), typeof(InteractableComponent)
            });
            _tablesIterator.Init(world);
        }

        public void Run()
        {
            foreach (var tableEntity in _tablesIterator)
            {
                ref var tableComponent = ref _workstationsAspect.GuestTablePool.Get(tableEntity);
                ref var holder = ref _playerAspect.HolderPool.Get(tableEntity);

                if (tableComponent.Guests is null || tableComponent.Guests.Count == 0) 
                    continue;

                var currentGuests = new List<ProtoEntity>();
                foreach (var packedGuest in tableComponent.Guests)
                    if (packedGuest.TryUnpack(out _, out var guestEntity))
                        currentGuests.Add(guestEntity);

                foreach (var guestEntity in currentGuests)
                {
                    if (TryServiceGuest(guestEntity, ref holder))
                    {
                        break; 
                    }
                }
            }
        }
        
        private bool TryServiceGuest(ProtoEntity guestEntity, ref HolderComponent holder)
        {
            ref var wantedItem = ref _guestAspect.WantedItemPool.Get(guestEntity).WantedItem;

            if (!wantedItem.Is(holder.Item))
                return false;

            if (_guestAspect.GuestServicedPool.Has(guestEntity))
                return false;
            _guestAspect.GuestServicedPool.Add(guestEntity);
            holder.Clear();
            Debug.Log("WINWINWIN");
            return true;
        }
    }
}