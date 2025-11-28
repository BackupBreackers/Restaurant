using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

public class ItemAspect : ProtoAspectInject
{
    public ProtoPool<MeatComponent> MeatPool;
    public ProtoPool<ItemComponent> ItemPool;
    public ProtoPool<RawMeatComponent> RawMeatPool;
}

public enum PickupItemType
{
    None = 0,
    RawMeat,
    Meat,
    Cheese,
}

public struct ItemComponent
{
    public PickupItemType PickupItemType;
}

[Serializable]
public struct RawMeatComponent
{
}

[Serializable]
public struct MeatComponent
{
}