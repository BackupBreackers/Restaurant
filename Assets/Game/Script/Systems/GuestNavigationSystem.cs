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
    [DI] private GuestGroupAspect _guestGroupAspect;

    private ProtoIt _groupsWithTablesIterator;
    private ProtoItExc _guestIterator;
    private ProtoIt _tableIterator;
    private ProtoItExc _leavingGuestIterator;

    public void Init(IProtoSystems systems)
    {
        _groupsWithTablesIterator = new(new[]
        {
            typeof(GuestGroupTag), typeof(GroupGotTableEvent)
        });
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
        _groupsWithTablesIterator.Init(_world);
        _guestIterator.Init(_world);
        _tableIterator.Init(_world);
        _leavingGuestIterator.Init(_world);
    }

    public void Run()
    {
        foreach (var groupEntity in _groupsWithTablesIterator)
        {
            ref var packedGuests = ref _guestGroupAspect.GuestGroupPool
                .Get(groupEntity).includedGuests;
            if (!_guestGroupAspect.GuestGroupPool.Get(groupEntity).table.TryUnpack(out _, out var table))
            {
                Debug.LogWarning("Стол куда-то исчез...");
                continue;
            }
            ref var tableComponent = ref _workstationsAspect.GuestTablePool.Get(table);
            Debug.Log("Распределяем места за столом...");

            var i = 0;
            foreach (var guestEntity in packedGuests)
            {
                if (!guestEntity.TryUnpack(out _, out var guest))
                {
                    Debug.LogWarning("Гость не распакован!!");
                }
                ref var targetPos = ref _guestAspect.TargetPositionComponentPool.Get(guest);
                targetPos.Position = tableComponent.guestPlaces[i];
                _guestAspect.GuestIsWalkingTagPool.Add(guest);
                ++i;
            }
            _guestGroupAspect.GroupGotTableEventPool.Del(groupEntity);
        }
        foreach (var leavingGuestEntity in _leavingGuestIterator)
        {
            ref var targetPosition = ref _guestAspect.TargetPositionComponentPool.Get(leavingGuestEntity);
            
            targetPosition.Position = _exitTransform.position;
            _guestAspect.GuestIsWalkingTagPool.Add(leavingGuestEntity);
        }
    }
}
