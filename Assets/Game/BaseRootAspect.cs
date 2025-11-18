using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

class BaseRootAspect : ProtoAspectInject
{
    public ProtoPool<PositionComponent> PositionPool;
    
    public PlayerAspect PlayerAspect;
    private ProtoWorld _world;
}