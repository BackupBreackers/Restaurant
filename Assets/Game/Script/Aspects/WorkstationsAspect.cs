using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

internal class WorkstationsAspect : ProtoAspectInject
{
    public ProtoPool<TableComponent> TablePool;
    public ProtoPool<InteractedComponent> InteractedPool;
}

internal struct TableComponent { }
internal struct InteractedComponent
{
    public ProtoPackedEntityWithWorld Player;
}