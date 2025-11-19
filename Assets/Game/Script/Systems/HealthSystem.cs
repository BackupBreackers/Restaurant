using Leopotam.EcsProto;
using UnityEngine;

public class HealthSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    private PlayerAspect _playerAspect;
    private ProtoIt _iterator;

    public void Init(IProtoSystems systems)
    {
        ProtoWorld world = systems.World();
        _playerAspect = (PlayerAspect)world.Aspect(typeof(PlayerAspect));
        
        _iterator = new(new[] { typeof(HealthComponent) });
        _iterator.Init(world);
    }

    public void Run()
    {
        foreach (ProtoEntity entity in _iterator)
        {
            ref HealthComponent h = ref _playerAspect.HealthPool.Get(entity);
            h.HealthValue++;
        }
    }

    public void Destroy()
    {
        _playerAspect = null;
        _iterator = null;
    }
}