using Game.Script.Factories;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Script.DISystem
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Main>();
            builder.Register<GameResources>(Lifetime.Singleton);
            builder.Register<RecipeService>(Lifetime.Singleton);
            
            builder.Register<StoveSystemFactory>(Lifetime.Singleton);
            builder.Register<RefrigeratorSystemFactory>(Lifetime.Singleton);
            
            builder.RegisterFactory<RefrigeratorSystem>(container =>
                container.Resolve<RefrigeratorSystemFactory>().CreateProtoSystem, Lifetime.Singleton);
            
            builder.RegisterFactory<StoveSystem>(container =>
                container.Resolve<StoveSystemFactory>().CreateProtoSystem, Lifetime.Singleton);
            
            builder.Register<WorkstationsModule>(Lifetime.Singleton);
        }
    }
}
