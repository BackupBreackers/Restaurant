using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;

internal class PlacementAspect : ProtoAspectInject
{
    public ProtoPool<FurnitureComponent> FurniturePool;
    public ProtoPool<PlacementGridComponent> PlacementGridPool;
}

public enum FurnitureType
{
    None,
    Fridge,
    Table
}

[Serializable, ProtoUnityAuthoring("PlacementAspect/Furniture")]
internal struct FurnitureComponent
{
    public FurnitureType Type;
    public GameObject ThisGameObject;
}

[Serializable, ProtoUnityAuthoring("PlacementAspect/PlacementGrid")]
internal struct PlacementGridComponent
{
    public Vector3 GridStartPosition;
    public Vector2Int GridSize;
    public int CellSize;
    public FurnitureComponent[][] GridData;
}
