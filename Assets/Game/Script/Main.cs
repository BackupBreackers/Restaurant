using System.Linq;
using Game.Script.AI;
using Game.Script.Modules;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Ai.Utility;
using Leopotam.EcsProto.QoL;
using UnityEngine;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Physics2D;
using VContainer;

public class Main : MonoBehaviour
{
    //[SerializeField] private GameObject player;

    private IProtoSystems _mainSystems;
    private IProtoSystems _physicsSystem;
    private IProtoSystems _guestSystem;
    private ProtoWorld _world;
    
    private Canvas _canvas;

    [Inject]
    private void InitializeEcs(IObjectResolver container)
    {
        var physicsSystemModules = new ProtoModules(
            new AutoInjectModule(),
            new UnityModule(),
            new PhysicsModule(),
            new UnityPhysics2DModule());

        var mainSystemModules = new ProtoModules(
            new AutoInjectModule(),
            new UnityModule(),
            new PlayerModule(),
            container.Resolve<WorkstationsModule>(),
            new PlacementModule(),
            new GuestModule(),
            new AiUtilityModule(
                default,
                1,
                new IdleWalkSolver()));

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