using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

public class TableInteractionSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    private PlayerAspect _playerAspect;
    private WorkstationsAspect _workstationsAspect;
    private ProtoIt _iterator;
    private ProtoWorld _world;

    public void Init(IProtoSystems systems)
    {
        _world = systems.World();
        _playerAspect = (PlayerAspect)_world.Aspect(typeof(PlayerAspect));
        _workstationsAspect = (WorkstationsAspect)_world.Aspect(typeof(WorkstationsAspect));

        _iterator = new(new[] { typeof(HolderComponent), typeof(InteractedComponent) });
        _iterator.Init(_world);
    }

    public void Run()
    {
        foreach (ProtoEntity entityTable in _iterator)
        {
            ref var tableHolder = ref _playerAspect.HolderPool.Get(entityTable);
            ref var invokerPlayer = ref _workstationsAspect.InteractedPool.Get(entityTable);
            
            if (invokerPlayer.Player.TryUnpack(out var world, out var entityPlayer))//если энтити игрока еще жива 
            {
                ref var playerHolder = ref _playerAspect.HolderPool.Get(entityPlayer);
                
                if (playerHolder.Entity == default)//если у игрока ничего не в руках
                {
                    if (tableHolder.Entity == default)// если на столе ничего нет
                    {
                        Debug.Log("И на руках и на столе пусто!");
                    }
                    else //На столе есть что-то
                    {
                        Debug.Log("Берем со стола!");
                        playerHolder.Entity = tableHolder.Entity;
                        tableHolder.Entity = default;
                    }
                }
                else if (playerHolder.Entity.TryUnpack(out var worldD, out var playerEntityItem)) //если на руках что-то есть
                {
                    if (tableHolder.Entity == default)// если на столе ничего нет
                    {
                        Debug.Log("Ложим на стол!");
                        
                        tableHolder.Entity = playerHolder.Entity;
                        playerHolder.Entity = default;
                        
                    }
                    else //На столе есть что-то
                    {
                        Debug.Log("И в руках и на столе что-то есть!");
                    }
                }
            }
           
            _workstationsAspect.InteractedPool.DelIfExists(entityTable);
        }
    }

    public void Destroy()
    {
        _playerAspect = null;
        _iterator = null;
    }
}