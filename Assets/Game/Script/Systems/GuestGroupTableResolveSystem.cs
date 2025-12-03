using System.Collections.Generic;
using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

namespace Game.Script.Systems
{
    public class GuestGroupTableResolveSystem : IProtoInitSystem, IProtoRunSystem
    {
        [DI] private ProtoWorld _world;
        [DI] private GuestAspect _guestAspect;
        [DI] private WorkstationsAspect _workstationsAspect;
        [DI] private GuestGroupAspect _guestGroupAspect;
        
        private ProtoIt _groupIterator;
        private ProtoIt _freeTablesIterator;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.World();
            _groupIterator = new(new[]
            {
                typeof(GuestGroupTag), typeof(GroupNeedsTableTag)
            });
            _freeTablesIterator = new(new[]
            {
                typeof(GuestTableComponent), typeof(GuestTableIsFreeTag)
            });
            _groupIterator.Init(_world);
            _freeTablesIterator.Init(_world);
        }

        public void Run()
        {
            foreach (var guestGroupEntity in _groupIterator)
            {
                foreach (var tableEntity in _freeTablesIterator)
                {
                    ref var group = ref _guestGroupAspect.GuestGroupPool.Get(guestGroupEntity);
                    ref var table = ref _workstationsAspect.GuestTablePool.Get(tableEntity);
                    group.table = _world.PackEntityWithWorld(tableEntity);
                    table.Guests = group.includedGuests;
                    _guestAspect.GuestTableIsFreeTagPool.Del(tableEntity);
                    _guestGroupAspect.GroupNeedsTablePool.Del(guestGroupEntity);
                    _guestGroupAspect.GroupGotTableEventPool.Add(guestGroupEntity);
                    Debug.Log("Выдали группе стол");
                    break;
                }   
            }
        }
    }
}