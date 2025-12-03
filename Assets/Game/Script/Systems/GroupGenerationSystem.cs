using System.Collections.Generic;
using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Script.Systems
{
    public class GroupGenerationSystem : IProtoInitSystem, IProtoRunSystem
    {
        [DI] private GuestGroupAspect _guestGroupAspect;
        [DI] private GuestAspect _guestAspect;
        
        private readonly GameObject _guestPrefab;
        private ProtoIt _groupsToGenerateIterator;
        
        public GroupGenerationSystem(GameObject guestPrefab)
        {
            this._guestPrefab = guestPrefab;
        }

        public void Init(IProtoSystems systems)
        {
            var world = systems.World();
            _groupsToGenerateIterator = new(new[]
            {
                typeof(GuestGroupTag), typeof(TargetGroupSize)
            });
            _groupsToGenerateIterator.Init(world);
        }

        public void Run()
        {
            foreach (var groupEntity in _groupsToGenerateIterator)
            {
                var numberOfGuests = _guestGroupAspect.TargetGroupSizePool.Get(groupEntity).size;
                var guests = CreateGuests(numberOfGuests);
                foreach (var guest in guests)
                    _guestAspect.GuestGroupComponentPool.Add(groupEntity);
                ref var groupGuests = ref _guestGroupAspect.GuestGroupPool.Get(groupEntity);
                groupGuests.includedGuests = guests;
                _guestGroupAspect.TargetGroupSizePool.Del(groupEntity);
            }
        }
        
        private List<ProtoPackedEntityWithWorld> CreateGuests(int numberOfGuests = 1)
        {
            var guests = new List<ProtoPackedEntityWithWorld>();
            for (var i = 0; i < numberOfGuests; ++i)
            {
                var go = GameObject.Instantiate(_guestPrefab);
                var authoring = go.GetComponent<MyAuthoring>();

                authoring.ProcessAuthoring();

                guests.Add(authoring.Entity());
            }
            return guests;
        }
    }
}