using System;
using Game.Script;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

internal class WorkstationsAspect : ProtoAspectInject
{
    public ProtoPool<PickPlaceEvent> PickPlaceEventPool;
    public ProtoPool<WorkstationsTypeComponent> WorkstationsTypePool;

    public ProtoPool<ItemPlaceEvent> ItemPlaceEventPool;
    public ProtoPool<ItemPickEvent> ItemPickEventPool;
    public ProtoPool<ItemSourceComponent> ItemSourcePool;
    public ProtoPool<StoveComponent> StovePool;
}

[Serializable, ProtoUnityAuthoring("WorkstationsAspect/WorkstationsTypeComponent")]
internal struct WorkstationsTypeComponent
{
    [SerializeReference, SubclassSelector]
    public WorkstationItem WorkstationType;
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
    public PickableItem resourceItemType;
}

internal struct PickPlaceEvent
{
    public ProtoPackedEntityWithWorld Invoker;
}