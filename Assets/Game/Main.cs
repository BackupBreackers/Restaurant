using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;
using Leopotam.EcsProto.Unity;

public class Main : MonoBehaviour
{
    IProtoSystems _systems;
    ProtoWorld _world;

    void Start()
    {
        var baseRootAspect = new BaseRootAspect();
        _world = new ProtoWorld(baseRootAspect);
        _systems = new ProtoSystems(_world)
            .AddModule(new AutoInjectModule())
            .AddModule(new UnityModule())
            .AddModule(new PlayerModule());
        _systems.Init();

        PlayerAspect playerAspect = (PlayerAspect)_world.Aspect(typeof(PlayerAspect));
        var playerAspectHealthPool = playerAspect.HealthPool;
        var playerAspectEEE = playerAspect.InputRawPool;

        ref HealthComponent c1 = ref playerAspectHealthPool.NewEntity(out ProtoEntity entity);
        ref InputRawComponent c2 = ref playerAspectEEE.Add (entity);
        ref PositionComponent c3 = ref baseRootAspect.PositionPool.Add(entity);
    }

    void Update()
    {
        _systems.Run();
    }

    void OnDestroy()
    {
        // Очистка систем.
        _systems?.Destroy();
        _systems = null;

        // Очистка дополнительных миров.

        // Очистка основного мира.
        _world?.Destroy();
        _world = null;
    }
}

public struct HealthComponent
{
    public float HealthValue;
}

public struct PositionComponent
{
    public Vector2 Position;
}

public struct InputRawComponent
{
    public Vector2 RawInput;
}