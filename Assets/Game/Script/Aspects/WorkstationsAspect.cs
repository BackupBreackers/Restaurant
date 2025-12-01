using System;
using System.Collections.Generic;
using Game.Script;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

public class WorkstationsAspect : ProtoAspectInject
{
    public ProtoPool<PickPlaceEvent> PickPlaceEventPool;
    public ProtoPool<ItemPlaceEvent> ItemPlaceEventPool;
    public ProtoPool<ItemPickEvent> ItemPickEventPool;
    public ProtoPool<PlaceWorkstationEvent> PlaceWorkstationEventPool;
    public ProtoPool<InteractedEvent> InteractedEventPool;
    
    
    
    public ProtoPool<WorkstationsTypeComponent> WorkstationsTypePool;
    public ProtoPool<ItemSourceComponent> ItemSourcePool;
    public ProtoPool<StoveComponent> StovePool;
    public ProtoPool<GuestTableComponent> GuestTablePool;
}
public struct InteractedEvent 
{
}
[Serializable]
public struct PlaceWorkstationEvent : IComponent
{
}

[Serializable]
public struct WorkstationsTypeComponent : IComponent
{
    [SerializeReference, SubclassSelector] public WorkstationItem workstationType;
}

public struct PickPlaceEvent
{
    public ProtoPackedEntityWithWorld Invoker;
}

public struct ItemPickEvent
{
}

public struct ItemPlaceEvent
{
}

[Serializable]
public struct InteractableComponent : IComponent
{
    public SpriteRenderer SpriteRenderer;
    public SpriteOutlineController OutlineController;
}


[Serializable]
public struct StoveComponent : IComponent
{
}

[Serializable]
public struct ItemSourceComponent : IComponent
{
    [SerializeReference, SubclassSelector] public PickableItem resourceItemType;
}

[Serializable]
public struct GuestTableComponent : IComponent
{
    public Vector2[] guestPlaces;
    public List<ProtoPackedEntityWithWorld> Guests;
}