using System;
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
    }

    
    [Serializable, ProtoUnityAuthoring("GuestAspect/TargetPositionComponent")]
    public struct TargetPositionComponent
    {
        public Vector2 Position;
    }
}