using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

public class RefrigeratorSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    [DI] readonly WorkstationsAspect _workstationsAspect = default;
    [DI] readonly PlayerAspect _playerAspect = default;
    [DI] readonly ItemAspect _itemAspect = default;

    private readonly Sprite _meatSprite;
    private ProtoIt _iterator;
    private ProtoWorld _world;

    public RefrigeratorSystem(Sprite meatSprite) =>
        this._meatSprite = meatSprite;

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

            if (playerHolder.ItemType != PickupItemType.None)
            {
                Debug.Log("Руки заняты, не могу взять мясо!");
            }
            else
            {
                SpawnItemForPlayer(itemSource.resourceItemType, playerEntity, ref playerHolder);
            }

            _workstationsAspect.PickPlaceEventPool.DelIfExists(sourceEntity);
        }
    }

    private void SpawnItemForPlayer(PickupItemType itemType, ProtoEntity playerEntity, ref HolderComponent playerHolder)
    {
        switch (itemType)
        {
            case PickupItemType.RawMeat:
                Debug.Log("Создано Мясо!");
                playerHolder.ItemType = PickupItemType.RawMeat;
                playerHolder.SpriteRenderer.sprite = _meatSprite;
                break;

            case PickupItemType.Cheese:
                Debug.Log("Создан Сыр!");
                break;
        }
        _playerAspect.HasItemTagPool.Add(playerEntity);
    }

    public void Destroy()
    {
        _iterator = null;
    }
}