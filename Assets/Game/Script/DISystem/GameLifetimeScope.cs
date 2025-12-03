using Game.Script.Factories;
using Game.Script.Systems;
using Leopotam.EcsProto.QoL;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Script.DISystem
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private Grid grid;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Main>();
            builder.Register<GameResources>(Lifetime.Singleton);
            builder.Register<RecipeService>(Lifetime.Singleton);
            builder.Register<PickableService>(Lifetime.Singleton);

            builder.RegisterComponent(grid);
            builder.Register<PlacementGrid>(Lifetime.Singleton);
            
            
            builder.Register<StoveSystemFactory>(Lifetime.Singleton);
            builder.Register<RefrigeratorSystemFactory>(Lifetime.Singleton);
            builder.Register<PhysicsEventsHandlerSystemFactory>(Lifetime.Singleton);
            builder.Register<SyncUnityPhysicsToEcsSystemFactory>(Lifetime.Singleton);
            builder.Register<PickPlaceSystemFactory>(Lifetime.Singleton);
            builder.Register<ClearSystemFactory>(Lifetime.Singleton);
            builder.Register<PlayerSpawnFurnitureSystemFactory>(Lifetime.Singleton);
            builder.Register<CreateGameObjectsSystemFactory>(Lifetime.Singleton);
            builder.Register<MoveFurnitureSystemFactory>(Lifetime.Singleton);
            builder.Register<MoveGameObjectSystemFactory>(Lifetime.Singleton);
            builder.Register<SyncGridPositionSystemFactory>(Lifetime.Singleton);
            
            builder.RegisterFactory<RefrigeratorSystem>(container =>
                container.Resolve<RefrigeratorSystemFactory>().CreateProtoSystem, Lifetime.Singleton);
            
            builder.RegisterFactory<StoveSystem>(container =>
                container.Resolve<StoveSystemFactory>().CreateProtoSystem, Lifetime.Singleton);
            
            builder.RegisterFactory<PhysicsEventsHandlerSystem>(container =>
                container.Resolve<PhysicsEventsHandlerSystemFactory>().CreateProtoSystem, Lifetime.Singleton);
            
            builder.RegisterFactory<SyncUnityPhysicsToEcsSystem>(container =>
                container.Resolve<SyncUnityPhysicsToEcsSystemFactory>().CreateProtoSystem, Lifetime.Singleton);
            
            builder.RegisterFactory<PickPlaceSystem>(container =>
                container.Resolve<PickPlaceSystemFactory>().CreateProtoSystem, Lifetime.Singleton);
            
            builder.RegisterFactory<ClearSystem>(container =>
                container.Resolve<ClearSystemFactory>().CreateProtoSystem, Lifetime.Singleton);

            builder.RegisterFactory<PlayerSpawnFurnitureSystem>(container =>
                container.Resolve<PlayerSpawnFurnitureSystemFactory>().CreateProtoSystem, Lifetime.Singleton);

            builder.RegisterFactory<CreateGameObjectsSystem>(container =>
                container.Resolve<CreateGameObjectsSystemFactory>().CreateProtoSystem, Lifetime.Singleton);

            builder.RegisterFactory<MoveFurnitureSystem>(container =>
                container.Resolve<MoveFurnitureSystemFactory>().CreateProtoSystem, Lifetime.Singleton);

            builder.RegisterFactory<MoveGameObjectSystem>(container =>
                container.Resolve<MoveGameObjectSystemFactory>().CreateProtoSystem, Lifetime.Singleton);

            builder.RegisterFactory<SyncGridPositionSystem>(container =>
                container.Resolve<SyncGridPositionSystemFactory>().CreateProtoSystem, Lifetime.Singleton);

            builder.Register<WorkstationsModule>(Lifetime.Singleton);
            builder.Register<PlacementModule>(Lifetime.Singleton);
            builder.Register<PhysicsModule>(Lifetime.Singleton);
        }
    }
}
