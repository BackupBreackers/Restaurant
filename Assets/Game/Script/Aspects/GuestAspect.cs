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
    }
    
    [Serializable, ProtoUnityAuthoring("GuestAspect/GuestTag")]
    public struct GuestTag
    {}
    
    public struct GuestArrivedEvent
    {}

    public struct GuestLeavingEvent
    {
        public Vector2 Position;
    }
    
    [Serializable, ProtoUnityAuthoring("GuestAspect/TargetPositionComponent")]
    public struct TargetPositionComponent
    {
        public Vector2 Position;
    }
}