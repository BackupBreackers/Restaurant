using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Script
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private Canvas canvas;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Main>();
            builder.RegisterComponent(canvas);
        }
    }
}
