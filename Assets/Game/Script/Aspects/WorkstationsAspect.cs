using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;
using UnityEngine.Serialization;

internal class WorkstationsAspect : ProtoAspectInject
{
    public ProtoPool<PickPlaceEvent> PickPlaceEventPool;
    public ProtoPool<WorkstationsTypeComponent> WorkstationsTypePool;
    

    public ProtoPool<ItemPlaceEvent> ItemPlaceEventPool;
    public ProtoPool<ItemPickEvent> ItemPickEventPool;
    public ProtoPool<ProcessingComponent> ProcessingPool;
    public ProtoPool<ItemSourceComponent> ItemSourcePool;
    public ProtoPool<StoveComponent> StovePool;
}

public enum WorkstationsType
{
    None = 0,
    Stove,
    Refrigerator
}
[Serializable, ProtoUnityAuthoring("WorkstationsAspect/WorkstationsTypeComponent")]
internal struct WorkstationsTypeComponent
{
    public WorkstationsType WorkstationType;
}

internal struct ProcessingComponent
{
}

internal struct ItemPickEvent
{
}

internal struct ItemPlaceEvent
{
}

[Serializable, ProtoUnityAuthoring("WorkstationsAspect/InteractableComponent")]
public struct InteractableComponent
{
    public SpriteRenderer SpriteRenderer;
    public SpriteOutlineController OutlineController;
}


[Serializable, ProtoUnityAuthoring("WorkstationsAspect/Stove")]
public struct StoveComponent
{
}

[Serializable, ProtoUnityAuthoring("WorkstationsAspect/ItemSource")]
internal struct ItemSourceComponent
{
    [FormerlySerializedAs("resourceItem")] public PickupItemType resourceItemType;
}

internal struct PickPlaceEvent
{
    public ProtoPackedEntityWithWorld Invoker;
}