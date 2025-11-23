using Leopotam.EcsProto;
using UnityEngine;

public class RandomSpawnFurnitureSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    private ProtoIt _iteratorPlayer;
    private PlayerAspect _playerAspect;
    private ProtoWorld _world;

    public void Init(IProtoSystems systems)
    {
        _world = systems.World();
        _playerAspect = (PlayerAspect)_world.Aspect(typeof(PlayerAspect));

        _iteratorPlayer = new(new[] { typeof(PlayerInputComponent), typeof(PositionComponent) });

        _iteratorPlayer.Init(_world);
    }

    public void Run()
    {
        foreach (var entityPlayer in _iteratorPlayer)
        {
            ref var playerInput = ref _playerAspect.InputRawPool.Get(entityPlayer);
            if (!playerInput.RandomSpawnFurniturePressed) continue;

            Debug.Log("P была нажата");
        }
    }

    public void Destroy()
    {
        _iteratorPlayer = null;
    }
}
