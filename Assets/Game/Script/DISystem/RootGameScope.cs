using VContainer;
using VContainer.Unity;

namespace Game.Script.DISystem
{
    public class RootGameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneController>(Lifetime.Singleton);
        }
    }
}