using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;

internal class PhysicsAspect : ProtoAspectInject
{
    public ProtoPool<PositionComponent> PositionPool;
    public ProtoPool<Rigidbody2DComponent> Rigidbody2DPool;
    public ProtoPool<MovementSpeedComponent> MovementSpeedPool;
    public ProtoPool<GridPositionComponent> GridPositionPool;
}

[Serializable, ProtoUnityAuthoring("PhysicsAspect/PositionComponent")]
public struct PositionComponent : IComponent
{
    public Vector2 Position;
}

[Serializable, ProtoUnityAuthoring("PhysicsAspect/GridPositionComponent")]
public struct GridPositionComponent
{
    public Vector2Int Position;
}

[Serializable, ProtoUnityAuthoring("PhysicsAspect/Rigidbody2DComponent")]
public struct Rigidbody2DComponent : IComponent
{
    public Rigidbody2D Rigidbody2D;
}

[Serializable, ProtoUnityAuthoring("PhysicsAspect/MovementSpeedComponent")]
public struct MovementSpeedComponent
{
    public float Value;
}