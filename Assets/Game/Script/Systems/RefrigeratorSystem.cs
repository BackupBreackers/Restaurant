using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;
using VContainer;

public class RefrigeratorSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    [DI] readonly WorkstationsAspect _workstationsAspect = default;
    [DI] readonly PlayerAspect _playerAspect = default;
    readonly Sprite _meatSprite;
    
    private ProtoIt _iterator;
    private ProtoWorld _world;
    
    public RefrigeratorSystem(Sprite meatSprite) =>
        this._meatSprite = meatSprite;

    public void Init(IProtoSystems systems)
    {
        _world = systems.World();
        _iterator = new(new[] { typeof(ItemSourceComponent), typeof(InteractedEvent) });
        _iterator.Init(_world);
    }

    public void Run()
    {
        foreach (ProtoEntity sourceEntity in _iterator)
        {
            ref var itemSource = ref _workstationsAspect.ItemSourcePool.Get(sourceEntity);
            ref var interacted = ref _workstationsAspect.InteractedEventPool.Get(sourceEntity);
            
            // 1. Получаем игрока
            if(!interacted.Invoker.TryUnpack(out _, out var playerEntity))
            {
                 _workstationsAspect.InteractedEventPool.DelIfExists(sourceEntity);
                continue;
            }

            ref var playerHolder = ref _playerAspect.HolderPool.Get(playerEntity);

            // 2. Проверка: Пустые ли руки у игрока?
            // Если в руках уже что-то есть, мы не можем взять новое мясо
            if (playerHolder.Entity.TryUnpack(out _, out _)) 
            {
                Debug.Log("Руки заняты, не могу взять мясо!");
            }
            else
            {
                // 3. Руки пусты -> Спавним ресурс!
                SpawnItemForPlayer(itemSource.ResourceType, playerEntity, ref playerHolder);
            }

            // Удаляем событие клика
            _workstationsAspect.InteractedEventPool.DelIfExists(sourceEntity);
        }
    }

    private void SpawnItemForPlayer(ItemId type, ProtoEntity playerEntity, ref HolderComponent playerHolder)
    {
        // A. Создаем новую сущность в мире
        ref var itemEntity = ref _workstationsAspect.ItemPool.NewEntity(out var newItemEntity);

        // B. Настраиваем её в зависимости от типа

        switch (type)
        {
            case ItemId.Meat:
                Debug.Log("Создано Мясо!");
                _workstationsAspect.MeatPool.Add(newItemEntity);
                ref var view = ref _workstationsAspect.ViewPool.Add(newItemEntity);
                itemEntity.ItemId = ItemId.Meat;
                Debug.Log(_meatSprite);
                // var newTrans = GameObject.Instantiate(_meatPrefab).transform;

                //view.GameObjectTransform = newTrans;
                break;
                
            case ItemId.Cheese:
                 Debug.Log("Создан Сыр!");
                 break;
        }

        // C. Кладем предмет в руки игроку
        playerHolder.Entity = _world.PackEntityWithWorld(newItemEntity);
        
        // Не забываем обновить тег (если ты решил его оставить)
        _playerAspect.HasItemTagPool.Add(playerEntity);
    }

    public void Destroy()
    {
        _iterator = null;
    }
}