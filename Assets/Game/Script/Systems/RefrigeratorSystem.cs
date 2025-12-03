using System;
using Game.Script;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;
using Object = System.Object;

public class RefrigeratorSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    [DI] readonly WorkstationsAspect _workstationsAspect;
    [DI] readonly PlayerAspect _playerAspect;
    [DI] private ProtoWorld _world;

    private ProtoIt _refrigeratorIt;
    private readonly PickableService _pickableService;

    public RefrigeratorSystem(PickableService pickableService) =>
        _pickableService = pickableService;

    public void Init(IProtoSystems systems)
    {
        _refrigeratorIt = new(new[] { typeof(ItemSourceComponent), typeof(PickPlaceEvent) });
        _refrigeratorIt.Init(_world);
    }

    public void Run()
    {
        foreach (var refrigeratorEntity in _refrigeratorIt)
        {
            ref var itemSource = ref _workstationsAspect.ItemSourcePool.Get(refrigeratorEntity);
            ref var interacted = ref _workstationsAspect.PickPlaceEventPool.Get(refrigeratorEntity);

            if (!interacted.Invoker.TryUnpack(out _, out var playerEntity)) 
                continue;
            
            if (!_playerAspect.HolderPool.Has(playerEntity)) 
                continue;
            
            ref var playerHolder = ref _playerAspect.HolderPool.Get(playerEntity);
            if (playerHolder.Item is not null)
            {
                Debug.Log($"[{refrigeratorEntity}] Руки заняты, не могу взять {itemSource.resourceItemType.GetType().Name}!");
                continue; 
            }
            
            var resourceType = itemSource.resourceItemType.GetType();
            
            if (_pickableService.TryGetPickable(resourceType, out var pickableItem))
                Helper.CreateItem(playerEntity, ref playerHolder, _playerAspect, pickableItem);
            else
                Debug.LogError($"Не удалось найти PickableItem для типа {resourceType.Name}!");
        }
    }


    public void Destroy()
    {
        _refrigeratorIt = null;
    }
}