using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity;

class UpdateInputSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem    
{
    [DIUnity ("InputService")] readonly InputService _inputService = default;
    
    private PlayerAspect _playerAspect;
        
    private ProtoIt _iterator;

    public void Init(IProtoSystems systems)
    {
        ProtoWorld world = systems.World();
        _playerAspect = (PlayerAspect)world.Aspect(typeof(PlayerAspect));
        
        _iterator = new(new[] { typeof(PlayerInputComponent) });
        _iterator.Init(world);
    }

    public void Run()
    {
        foreach (ProtoEntity entity in _iterator)
        {
            ref PlayerInputComponent playerInputComponent = ref _playerAspect.InputRawPool.Get(entity);
            playerInputComponent.MoveDirection = _inputService.MoveDirection;
        }
    }

    public void Destroy()
    {
        _playerAspect = null;
        _iterator = null;
    }
}