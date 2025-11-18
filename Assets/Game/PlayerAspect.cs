using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

class PlayerAspect : ProtoAspectInject
{
    public ProtoPool<InputRawComponent> InputRawPool;
    public ProtoPool<HealthComponent> HealthPool { get; private set; }
    private ProtoWorld _world;
}

