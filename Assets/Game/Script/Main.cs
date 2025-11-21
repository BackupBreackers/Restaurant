using System.Linq;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Physics2D;
using Unity.VisualScripting;
using VContainer;

public class Main : MonoBehaviour
{
    //[SerializeField] private GameObject player;

    private IProtoSystems _mainSystems;
    private IProtoSystems _physicsSystem;
    private ProtoWorld _world;
    
    private Canvas _canvas;

    [Inject]
    private void InitializeEcs(Canvas canvas)
    {
        this._canvas = canvas;
        Debug.Log(_canvas);
        var physicsSystemModules = new ProtoModules(
            new AutoInjectModule(),
            new UnityModule(),
            new PhysicsModule(),
            new UnityPhysics2DModule());

        var mainSystemModules = new ProtoModules(
            new AutoInjectModule(),
            new UnityModule(),
            new PlayerModule(),
            new InteractionModule());

        var combinedModules = new ProtoModules(physicsSystemModules.Modules()
            .Concat(mainSystemModules.Modules())
            .ToArray());


        _world = new ProtoWorld(combinedModules.BuildAspect());


        _physicsSystem = new ProtoSystems(_world)
            .AddModule(physicsSystemModules.BuildModule());
        _physicsSystem.Init();

        _mainSystems = new ProtoSystems(_world)
            .AddModule(mainSystemModules.BuildModule());
        _mainSystems.Init();


        // var playerAspect = (PlayerAspect)_world.Aspect(typeof(PlayerAspect));
        // var physicsAspect = (PhysicsAspect)_world.Aspect(typeof(PhysicsAspect));
        //
        // ref HealthComponent c1 = ref playerAspect.HealthPool.NewEntity(out ProtoEntity entity);
        // ref PlayerInputComponent c2 = ref playerAspect.InputRawPool.Add(entity);
        // ref PositionComponent c3 = ref physicsAspect.PositionPool.Add(entity);
        // ref Rigidbody2DComponent c4 = ref physicsAspect.Rigidbody2DPool.Add(entity);
        // ref MovementSpeedComponent speed = ref playerAspect.SpeedPool.Add(entity);
        //
        // speed.Value = 10;
        // c4.Rigidbody2D = player.GetOrAddComponent<Rigidbody2D>();
    }

    void Update()
    {
        _mainSystems.Run();
    }

    void FixedUpdate()
    {
        _physicsSystem.Run();
    }

    void OnDestroy()
    {
        // Очистка систем.
        _mainSystems?.Destroy();
        _mainSystems = null;

        // Очистка дополнительных миров.

        // Очистка основного мира.
        _world?.Destroy();
        _world = null;
    }
}