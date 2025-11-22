using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;

internal class WorkstationsAspect : ProtoAspectInject
{
    public ProtoPool<InteractedEvent> InteractedEventPool;
    public ProtoPool<ItemSourceComponent> ItemSourcePool;
    public ProtoPool<MeatComponent> MeatPool;
    public ProtoPool<ItemComponent> ItemPool;
    public ProtoPool<ViewComponent> ViewPool;
}
internal struct ViewComponent
{
    public Transform GameObjectTransform;
}

internal struct ItemComponent
{
    public ItemId  ItemId;
}

internal struct MeatComponent
{
}

public enum ItemId
{
    None = 0,
    Meat,
    Cheese,
    Bun,
    // ... другие
}

[Serializable, ProtoUnityAuthoring("WorkstationsAspect/ItemSource")]
internal struct ItemSourceComponent
{
    public ItemId ResourceType; // <-- Вот тут мы указываем "Meat" в инспекторе
}

internal struct InteractedEvent
{
    public ProtoPackedEntityWithWorld Invoker;
}