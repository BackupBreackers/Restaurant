using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

class PlayerAspect : ProtoAspectInject
{
    public ProtoPool<PlayerInputComponent> InputRawPool;
    public ProtoPool<HealthComponent> HealthPool;
    public ProtoPool<MovementSpeedComponent> SpeedPool;
}

