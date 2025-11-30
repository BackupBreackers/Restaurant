using System;
using System.Collections.Generic;
using Game.Script;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;

internal class WorkstationsAspect : ProtoAspectInject
{
    public ProtoPool<PickPlaceEvent> PickPlaceEventPool;
    public ProtoPool<WorkstationsTypeComponent> WorkstationsTypePool;

    public ProtoPool<ItemPlaceEvent> ItemPlaceEventPool;
    public ProtoPool<ItemPickEvent> ItemPickEventPool;
    public ProtoPool<ItemSourceComponent> ItemSourcePool;
    public ProtoPool<StoveComponent> StovePool;
    public ProtoPool<GuestTable> GuestTablePool;
}

[Serializable, ProtoUnityAuthoring("WorkstationsAspect/WorkstationsTypeComponent")]
public struct WorkstationsTypeComponent : IComponent
{
    [SerializeReference, SubclassSelector]
    public WorkstationItem workstationType;
}

public struct ItemPickEvent
{
}

public struct ItemPlaceEvent
{
}

[Serializable, ProtoUnityAuthoring("WorkstationsAspect/InteractableComponent")]
public struct InteractableComponent : IComponent
{
    public SpriteRenderer SpriteRenderer;
    public SpriteOutlineController OutlineController;
}


[Serializable, ProtoUnityAuthoring("WorkstationsAspect/Stove")]
public struct StoveComponent : IComponent
{
}

[Serializable, ProtoUnityAuthoring("WorkstationsAspect/ItemSource")]
public struct ItemSourceComponent : IComponent
{
    [SerializeReference, SubclassSelector]
    public PickableItem resourceItemType;   
}

public interface IComponent
{
}

public struct PickPlaceEvent
{
    public ProtoPackedEntityWithWorld Invoker;
}

[Serializable]
public struct GuestTable : IComponent
{
    public Vector2[] guestPlaces;
    public List<ProtoPackedEntityWithWorld> Guests;
}