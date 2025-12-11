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
    public ProtoPool<UnityTransformRef> UnityTransformPool;
}

[Serializable]
public struct PositionComponent : IComponent
{
    public Vector2 Position;
}

[Serializable]
public struct GridPositionComponent : IComponent
{
    public Vector2Int Position;
}

[Serializable]
public struct Rigidbody2DComponent : IComponent
{
    public Rigidbody2D Rigidbody2D;
}

[Serializable]
public struct MovementSpeedComponent : IComponent
{
    public float Value;
}

[Serializable]
public struct UnityTransformRef : IComponent
{
    public Transform Transform;
}
