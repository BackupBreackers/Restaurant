using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace Game.Script.Aspects
{
    public class GuestGroupAspect : ProtoAspectInject
    {
        public ProtoPool<GuestGroupTag> GuestGroupPool;
        public ProtoPool<TargetGroupSize>  TargetGroupSizePool;
        public ProtoPool<GroupNeedsTableTag> GroupNeedsTablePool;
        public ProtoPool<GroupGotTableEvent> GroupGotTableEventPool;
    }

    [Serializable]
    public struct GuestGroupServedEvent : IComponent
    {
    }

    [Serializable]
    public struct GroupGotTableEvent : IComponent
    {
    }

    [Serializable]
    public struct GuestGroupTag : IComponent
    {
        public List<ProtoPackedEntityWithWorld> includedGuests;
        public ProtoPackedEntityWithWorld table;
    }

    [Serializable]
    public struct GroupNeedsTableTag : IComponent
    {
    }

    [Serializable]
    public struct TargetGroupSize : IComponent
    {
        public int size;
    }
}