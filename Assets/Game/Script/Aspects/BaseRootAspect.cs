using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

class BaseRootAspect : ProtoAspectInject
{
    public PhysicsAspect PhysicsAspect;
    public PlayerAspect PlayerAspect;
    
    private ProtoWorld _world;
}