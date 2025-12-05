using Game.Script.Factories;
using Leopotam.EcsProto;
using System;
using UnityEngine;

public class PlacementModule : IProtoModule
{
    private PlayerSpawnFurnitureSystem playerSpawnFurnitureSystem;
    private CreateGameObjectsSystem createGameObjectsSystem;
    private MoveFurnitureSystem moveFurnitureSystem;
    private MoveGameObjectSystem moveGameObjectSystem;
    private SyncGridPositionSystem syncGridPositionSystem;

    public PlacementModule(PlayerSpawnFurnitureSystemFactory playerSpawnFurnitureSystem,
        CreateGameObjectsSystemFactory createGameObjectsSystem,
        MoveGameObjectSystemFactory moveGameObjectSystem,
        MoveFurnitureSystemFactory moveFurnitureSystem,
        SyncGridPositionSystemFactory syncGridPositionSystem)
    {
        this.playerSpawnFurnitureSystem = playerSpawnFurnitureSystem.CreateProtoSystem();
        this.createGameObjectsSystem = createGameObjectsSystem.CreateProtoSystem();
        this.moveFurnitureSystem = moveFurnitureSystem.CreateProtoSystem();
        this.moveGameObjectSystem = moveGameObjectSystem.CreateProtoSystem();
        this.syncGridPositionSystem = syncGridPositionSystem.CreateProtoSystem();
    }

    public void Init(IProtoSystems systems)
    {
        systems
            .AddSystem(playerSpawnFurnitureSystem)
            .AddSystem(createGameObjectsSystem)
            .AddSystem(moveFurnitureSystem)
            .AddSystem(moveGameObjectSystem)
            .AddSystem(syncGridPositionSystem)
            .AddSystem(new SpawnerInteractSystem());
    }

    public IProtoAspect[] Aspects()
    {
        return new IProtoAspect[] { new PlacementAspect() };
    }

    public Type[] Dependencies()
    {
        return null;
    }
}
