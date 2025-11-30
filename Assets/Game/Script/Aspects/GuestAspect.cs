using System;
using System.ComponentModel;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;

namespace Game.Script.Aspects
{
    public class GuestAspect : ProtoAspectInject
    {
        public ProtoPool<MovementSpeedComponent> MovementSpeedComponentPool;
        public ProtoPool<PositionComponent> PositionComponentPool;
        public ProtoPool<TargetPositionComponent> TargetPositionComponentPool;
        public ProtoPool<GuestArrivedEvent> GuestArrivedEventPool;
        public ProtoPool<GuestLeavingEvent> GuestLeavingEventPool;
        public ProtoPool<GuestTag> GuestTagPool;
        public ProtoPool<GuestTakeOrderEvent> GuestTakeOrderEventPool;
        public ProtoPool<InteractableComponent> InteractableComponentPool;
        public ProtoPool<DidPlayerInteracted> DidPlayerInteractedPool;
        public ProtoPool<DidGotOrder> DidGotOrderPool;
        public ProtoPool<GuestTable> GuestTablePool;
        public ProtoPool<GuestIsWalking> GuestIsWalkingPool;
        public ProtoPool<WantedItem> WantedItemPool;
    }
    
    [Serializable, ProtoUnityAuthoring("GuestAspect/GuestTag")]
    public struct GuestTag : IComponent
    {}
    
    [Serializable]
    public struct GuestIsWalking : IComponent
    {}
    
    public struct GuestArrivedEvent
    {}

    public struct GuestLeavingEvent
    {
        public Vector2 Position;
    }
    
    public struct GuestTakeOrderEvent
    {}

    public struct DidGotOrder
    { }
    
    public struct EbanayaHuinyaEvent{
        public ProtoPackedEntityWithWorld Invoker;}

    [Serializable, ProtoUnityAuthoring("GuestAspect/DidPlayerInteracted")]
    public struct DidPlayerInteracted : IComponent
    { }
    
    [Serializable, ProtoUnityAuthoring("GuestAspect/TargetPositionComponent")]
    public struct TargetPositionComponent : IComponent
    {
        public ProtoPackedEntityWithWorld Table;
        public Vector2 Position;
    }

    [Serializable]
    public struct WantedItem : IComponent
    {
        [SubclassSelector, SerializeReference]
        public PickableItem Item;
    }
}