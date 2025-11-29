using System;
using Game.Script;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

public class RefrigeratorSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    [DI] readonly WorkstationsAspect _workstationsAspect = default;
    [DI] readonly PlayerAspect _playerAspect = default;

    private readonly PickableService _pickableService;
    private ProtoIt _iterator;
    private ProtoWorld _world;

    public RefrigeratorSystem(PickableService pickableService) =>
        this._pickableService = pickableService;

    public void Init(IProtoSystems systems)
    {
        _world = systems.World();
        _iterator = new(new[] { typeof(ItemSourceComponent), typeof(PickPlaceEvent) });
        _iterator.Init(_world);
    }

    public void Run()
    {
        foreach (ProtoEntity sourceEntity in _iterator)
        {
            ref var itemSource = ref _workstationsAspect.ItemSourcePool.Get(sourceEntity);
            ref var interacted = ref _workstationsAspect.PickPlaceEventPool.Get(sourceEntity);

            if (!interacted.Invoker.TryUnpack(out _, out var playerEntity)) continue;

            ref var playerHolder = ref _playerAspect.HolderPool.Get(playerEntity);

            if (playerHolder.ItemType is not null)
            {
                Debug.Log("Руки заняты, не могу взять мясо!");
            }
            else
            {
                SpawnItemForPlayer(itemSource.resourceItemType.GetType(), playerEntity, ref playerHolder);
            }

            _workstationsAspect.PickPlaceEventPool.DelIfExists(sourceEntity);
        }
    }

    private void SpawnItemForPlayer(Type item, ProtoEntity playerEntity, ref HolderComponent playerHolder)
    {
        playerHolder.ItemType = item;
        Debug.Log("Spawn Item");
        if (_pickableService.TryGetPickable(item, out var pickable))
        {
            Debug.Log("Item Found");
            playerHolder.SpriteRenderer.sprite = pickable.PickupItemSprite;
            
        }
        _playerAspect.HasItemTagPool.Add(playerEntity);
    }

    public void Destroy()
    {
        _iterator = null;
    }
}