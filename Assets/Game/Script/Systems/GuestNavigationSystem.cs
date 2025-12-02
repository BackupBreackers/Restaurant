using System;
using System.Collections.Generic;
using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;

public class GuestNavigationSystem : IProtoInitSystem, IProtoRunSystem
{
    [DIUnity("Exit")] private readonly Transform _exitTransform = default;
    [DI] private ProtoWorld _world;
    [DI] private GuestAspect _guestAspect;
    [DI] private WorkstationsAspect _workstationsAspect;

    private ProtoItExc _guestIterator;
    private ProtoIt _tableIterator;
    private ProtoItExc _leavingGuestIterator;

    public void Init(IProtoSystems systems)
    {
        _guestIterator = new(new[]
        {
            typeof(GuestTag), typeof(TargetPositionComponent), 
        },new []
        {
            typeof(GuestIsWalkingTag), typeof(WaitingOrderTag), typeof(WaitingTakeOrderTag), typeof(GuestServicedTag)
        });
        _tableIterator = new(new[] { typeof(GuestTableComponent), typeof(GuestTableIsFreeTag) });
        _leavingGuestIterator = new(new[]
        {
            typeof(GuestTag), typeof(TargetPositionComponent), typeof(GuestServicedTag)
        },new []
        {
            typeof(GuestIsWalkingTag), typeof(WaitingOrderTag), typeof(WaitingTakeOrderTag)
        });
        _guestIterator.Init(_world);
        _tableIterator.Init(_world);
        _leavingGuestIterator.Init(_world);
    }

    public void Run()
    {
        foreach (var guestEntity in _guestIterator)
        {
            ref var targetPosition = ref _guestAspect.TargetPositionComponentPool.Get(guestEntity);

            foreach (var tableEntity in _tableIterator)
            {
                ref var tableComponent = ref _workstationsAspect.GuestTablePool.Get(tableEntity);

                tableComponent.Guests ??= new List<ProtoPackedEntityWithWorld>();
                tableComponent.Guests.Add(_world.PackEntityWithWorld(guestEntity));
                Debug.Log("ЗАСУНУЛИ");
                
                targetPosition.Position = tableComponent.guestPlaces[0];
                
                break;
            }
            _guestAspect.GuestIsWalkingTagPool.Add(guestEntity);
        }
        foreach (var leavingGuestEntity in _leavingGuestIterator)
        {
            ref var targetPosition = ref _guestAspect.TargetPositionComponentPool.Get(leavingGuestEntity);
            
            targetPosition.Position = _exitTransform.position;
            _guestAspect.GuestIsWalkingTagPool.Add(leavingGuestEntity);
        }
    }
}
