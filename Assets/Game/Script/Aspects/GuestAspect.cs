using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

namespace Game.Script.Aspects
{
    public class GuestAspect : ProtoAspectInject
    {
        public ProtoPool<TargetPositionComponent> TargetPositionComponentPool;
        public ProtoPool<GuestTableComponent> GuestTablePool;
        public ProtoPool<WantedItemComponent> WantedItemPool;
        
        public ProtoPool<GuestTag> GuestTagPool;
        public ProtoPool<WaitingTakeOrderTag>  WaitingTakeOrderTagPool;
        public ProtoPool<WaitingOrderTag> WaitingOrderTagPool;
        public ProtoPool<GuestServicedTag> GuestServicedPool;
        public ProtoPool<GuestTableIsFreeTag> GuestTableIsFreeTagPool;
        public ProtoPool<GuestIsWalkingTag> GuestIsWalkingTagPool;
        
        public ProtoPool<ReachedTargetPositionEvent> ReachedTargetPositionEventPool;
    }
    
    public struct WaitingOrderTag
    {
    }

    [Serializable]
    public struct GuestTag : IComponent
    {
    }
    
    [Serializable]
    public struct GuestTableIsFreeTag : IComponent
    {
    }

    [Serializable]
    public struct GuestIsWalkingTag : IComponent
    {
    }

    public struct ReachedTargetPositionEvent
    {
    }

    public struct GuestServicedTag
    {
    }

    [Serializable]
    public struct WaitingTakeOrderTag : IComponent
    {
    }

    [Serializable]
    public struct TargetPositionComponent : IComponent
    {
        public ProtoPackedEntityWithWorld Table;
        public Vector2 Position;
    }

    [Serializable]
    public struct WantedItemComponent : IComponent
    {
        [SubclassSelector, SerializeReference] public PickableItem WantedItem;
    }
}