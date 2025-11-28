using Game.Script;
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

        _tableIterator = new(new[] { typeof(HolderComponent), typeof(PickPlaceEvent) });
        _tableIterator.Init(_world);
    }

    public void Run()
    {
        foreach (var tableEntity in _tableIterator)
        {
            ref var pickPlaceEvent = ref _workstationsAspect.PickPlaceEventPool.Get(tableEntity);


            // Удаляем событие в конце кадра
            _workstationsAspect.PickPlaceEventPool.DelIfExists(tableEntity);
        }
    }


    public void Destroy()
    {
        _playerAspect = null;
        _tableIterator = null;
    }
}