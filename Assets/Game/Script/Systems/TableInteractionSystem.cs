using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

public class TableInteractionSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    private PlayerAspect _playerAspect;
    private WorkstationsAspect _workstationsAspect;
    private ProtoIt _tableIterator;
    private ProtoWorld _world;

    public void Init(IProtoSystems systems)
    {
        _world = systems.World();
        _playerAspect = (PlayerAspect)_world.Aspect(typeof(PlayerAspect));
        _workstationsAspect = (WorkstationsAspect)_world.Aspect(typeof(WorkstationsAspect));

        _tableIterator = new(new[] { typeof(HolderComponent), typeof(InteractedEvent) });
        _tableIterator.Init(_world);
    }

    public void Run()
    {
        foreach (var tableEntity in _tableIterator)
        {
            ref var interactedEvent = ref _workstationsAspect.InteractedEventPool.Get(tableEntity);

            // 1. Проверяем валидность игрока. Если игрок "мертв", пропускаем итерацию.
            if (!interactedEvent.Invoker.TryUnpack(out _, out var playerEntity))
            {
                _workstationsAspect.InteractedEventPool.DelIfExists(tableEntity);
                continue;
            }

            ref var tableHolder = ref _playerAspect.HolderPool.Get(tableEntity);
            ref var playerHolder = ref _playerAspect.HolderPool.Get(playerEntity);

            // 2. Определяем состояние через наличие тегов (или можно проверять Holder.Entity.IsAlive())
            bool playerHasItem = _playerAspect.HasItemTagPool.Has(playerEntity);
            bool tableHasItem = _playerAspect.HasItemTagPool.Has(tableEntity);

            // 3. Используем switch по кортежу для обработки 4-х ситуаций
            switch (playerHasItem, tableHasItem)
            {
                case (false, false):
                    Debug.Log("И на руках, и на столе пусто! (Ничего не делаем)");
                    break;

                case (false, true):
                    Debug.Log("Берем со стола!");
                    TransferItem(from: tableEntity, to: playerEntity, ref tableHolder, ref playerHolder);
                    break;

                case (true, false):
                    Debug.Log("Кладем на стол!");
                    TransferItem(from: playerEntity, to: tableEntity, ref playerHolder, ref tableHolder);
                    break;

                case (true, true):
                    Debug.Log("И в руках, и на столе что-то есть! (Свап или запрет)");
                    // Тут можно реализовать логику замены предметов или объединения (как в PlateUP с тарелками)
                    break;
            }

            // Удаляем событие в конце кадра
            _workstationsAspect.InteractedEventPool.DelIfExists(tableEntity);
        }
    }

// Вынесли логику переноса в helper-метод (или локальную функцию), чтобы не дублировать код
    private void TransferItem(ProtoEntity from, ProtoEntity to, ref HolderComponent fromHolder,
        ref HolderComponent toHolder)
    {
        // 1. Перекидываем ссылку на Entity предмета
        toHolder.Entity = fromHolder.Entity;
        fromHolder.Entity = default;

        // 2. Обновляем теги (если вы используете их для флагов)
        _playerAspect.HasItemTagPool.Add(to);
        _playerAspect.HasItemTagPool.DelIfExists(from);

        // Примечание: В ECS часто удобнее сразу тут отправить событие или обновить компонент View,
        // чтобы модель синхронизировалась с Unity GameObject (трансформом предмета).
    }

    public void Destroy()
    {
        _playerAspect = null;
        _tableIterator = null;
    }
}