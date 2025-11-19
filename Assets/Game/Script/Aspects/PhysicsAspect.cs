using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

internal class PhysicsAspect : ProtoAspectInject
{
    public ProtoPool<PositionComponent> PositionPool;
    public ProtoPool<Rigidbody2DComponent> Rigidbody2DPool;
}