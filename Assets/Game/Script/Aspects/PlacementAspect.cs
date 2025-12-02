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
    public ProtoPool<MoveThisFurnitureEvent> MoveThisFurnitureEventPool;
    public ProtoPool<CreateGameObjectEvent> CreateGameObjectEventPool;
    public ProtoPool<MoveThisGameObjectEvent> MoveThisGameObjectEventPool;
    public ProtoPool<SyncGridPositionEvent> SyncMyGridPositionEventPool;
}

public enum FurnitureType
{
    None,
    Fridge,
    Table
}
[Serializable, ProtoUnityAuthoring("PlacementAspect/PlacementTransformComponent")]
public struct PlacementTransformComponent : IComponent
{
    public Transform transform;
}
[Serializable, ProtoUnityAuthoring("PlacementAspect/FurnitureComponent")]
public struct FurnitureComponent : IComponent
{
    public FurnitureType type;
}

internal struct MoveThisFurnitureEvent
{
    public ProtoPackedEntityWithWorld Invoker;
}

internal struct CreateGameObjectEvent
{
    public FurnitureType furnitureType;
    public Vector2Int position;
}

internal struct MoveThisGameObjectEvent
{
    public Vector2Int newPositionInGrid;
}
[Serializable, ProtoUnityAuthoring("PlacementAspect/SyncGridPositionEvent")]
public struct SyncGridPositionEvent : IComponent
{
    public List<Vector2Int> entityGridPositions;
}