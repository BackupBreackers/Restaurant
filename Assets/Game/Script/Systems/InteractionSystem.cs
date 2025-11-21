using Leopotam.EcsProto;

internal class InteractionSystem : IProtoInitSystem, IProtoRunSystem
{
    private ProtoIt _iterator;
    private InteractionAspect _interactionAspect;

    public void Init(IProtoSystems systems)
    {
        ProtoWorld world = systems.World();
        _interactionAspect = (InteractionAspect)world.Aspect(typeof(InteractionAspect));

        
        _iterator = new(new[] { typeof(InteractableComponent) });
        _iterator.Init(world);
    }

    public void Run()
    {
        // foreach (var entity in _iterator)
        // {
        //     ref var interactable = ref _interactionAspect.InteractablePool.Get(entity);
        // }
    }
}
