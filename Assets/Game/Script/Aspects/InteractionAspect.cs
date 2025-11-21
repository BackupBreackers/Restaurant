using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;

internal class InteractionAspect : ProtoAspectInject
{
    public ProtoPool<HolderComponent> HolderPool;
    public ProtoPool<ItemComponent> ItemPool;
    public ProtoPool<InteractionFocusComponent> InteractionFocusPool;
    public ProtoPool<InteractableComponent> InteractablePool;
}

[Serializable, ProtoUnityAuthoring]
internal struct ItemComponent
{
    public int OwnerEntityId;
}

[Serializable, ProtoUnityAuthoring]
internal struct HolderComponent
{
    public int HeldEntityId; // ID сущности, которую держим (0 или -1, если пусто)
    public bool IsEmpty => HeldEntityId <= 0; // Хелпер для удобства
}

[Serializable, ProtoUnityAuthoring]
internal struct InteractionFocusComponent
{
    public int TargetEntityId; // ID стола перед лицом игрока
}

[Serializable, ProtoUnityAuthoring]
internal struct AllNearInteractablesComponent
{
    public List<SpriteOutlineController> AllNearInteractables;
}

[Serializable, ProtoUnityAuthoring]
internal struct InteractableComponent
{
    public SpriteOutlineController OutlineController;
}