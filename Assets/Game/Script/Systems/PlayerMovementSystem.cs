using Leopotam.EcsProto;

class PlayerMovementSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    private BaseRootAspect _baseRootAspect;
    private PlayerAspect _playerAspect;
        
    private ProtoIt _iterator;

    public void Init(IProtoSystems systems)
    {
        ProtoWorld world = systems.World();
        _playerAspect = (PlayerAspect)world.Aspect(typeof(PlayerAspect));
        
        _iterator = new(new[] { typeof(InputRawComponent), typeof(PositionComponent) });
        _iterator.Init(world);
        
        _baseRootAspect = (BaseRootAspect)world.Aspect(typeof(BaseRootAspect));
    }

    public void Run()
    {
        foreach (ProtoEntity entity in _iterator)
        {
            // ref InputRawComponent inputRawComponent = ref _playerAspect.InputRawPool.Get(entity);
            // ref PositionComponent posComponent = ref _baseRootAspect.PositionPool.Get(entity);
            // posComponent.Position += inputRawComponent.RawInput;
        }
    }

    public void Destroy()
    {
        _playerAspect = null;
        _iterator = null;
        _baseRootAspect = null;
    }
}