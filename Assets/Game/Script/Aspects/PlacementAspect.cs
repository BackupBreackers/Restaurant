using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;

internal class PlacementAspect : ProtoAspectInject
{
    public ProtoPool<FurnitureComponent> FurniturePool;
    public ProtoPool<PlacementTransformComponent> PlacementTransformPool;
    public ProtoPool<MoveThisFurnitureTag> MoveThisFurnitureEventPool;
    public ProtoPool<CreateGameObjectEvent> CreateGameObjectEventPool;
    public ProtoPool<MoveThisGameObjectEvent> MoveThisGameObjectEventPool;
    public ProtoPool<SyncGridPositionEvent> SyncMyGridPositionEventPool;
}

[Serializable]
public struct PlacementTransformComponent : IComponent
{
    public Transform transform;
}
[Serializable]
public struct FurnitureComponent : IComponent
{
    [SerializeReference,SubclassSelector]
    public WorkstationItem type;
}

public struct MoveThisFurnitureTag
{
    public ProtoPackedEntityWithWorld Invoker;
}

internal struct CreateGameObjectEvent
{
    public Type furnitureType;
    public Vector2Int position;
}

internal struct MoveThisGameObjectEvent
{
    public Vector2Int newPositionInGrid;
}

[Serializable]
public struct SyncGridPositionEvent : IComponent
{
    public List<Vector2Int> entityGridPositions;
}