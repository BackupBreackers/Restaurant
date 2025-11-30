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
        private ProtoIt _tables;
        public void Init(IProtoSystems systems)
        {
            var world = systems.World();
            _tables = new(new[]
            {
                typeof(GuestTable), typeof(PickPlaceEvent), typeof(HolderComponent),
                typeof(PositionComponent), typeof(InteractableComponent)
            });
            _tables.Init(world);
        }

        public void Run()
        {
            foreach (var entity in _tables)
            {
                ref var packedGuests =
                    ref _workstationsAspect.GuestTablePool.Get(entity).Guests;
                if (packedGuests is null)
                    continue;
                var guests = new List<ProtoEntity>();
                foreach (var guest in packedGuests)
                {
                    if (!guest.TryUnpack(out _, out var guestEntity))
                    {
                        Debug.Log("Гость не распакован!");
                        continue;
                    }
                    guests.Add(guestEntity);
                }
                ref var holder = ref _playerAspect.HolderPool.Get(entity);
                if (holder.ItemType == null)
                {
                    continue;
                }

                foreach (var guest in guests)
                {
                    if (_guestAspect.WantedItemPool.Get(guest).Item.GetType() == holder.ItemType
                        && !_guestAspect.DidGotOrderPool.Has(guest))
                    {
                        _guestAspect.DidGotOrderPool.Add(guest);
                        holder.ItemType = null;
                        holder.SpriteRenderer.sprite = null;
                        break;
                    }
                }
                var isAllGuestsServed = true;
                foreach (var guest in guests)
                {
                    if (!_guestAspect.DidGotOrderPool.Has(guest))
                    {
                        isAllGuestsServed = false;
                        break;
                    }
                }

                if (isAllGuestsServed)
                    ClearTable(entity, guests);
            }
        }

        private void ClearTable(ProtoEntity table, List<ProtoEntity> guests)
        {
            _workstationsAspect.GuestTablePool.Get(table).Guests.Clear();
            foreach (var guest in guests)
            {
                _baseAspect.TimerPool.Get(guest).Completed = true;
            }
        }
    }
}