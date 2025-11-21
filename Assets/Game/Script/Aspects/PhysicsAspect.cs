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
}

[Serializable, ProtoUnityAuthoring("PhysicsAspect/PositionComponent")]
public struct PositionComponent
{
    public Vector2 Position;
}

[Serializable, ProtoUnityAuthoring("PhysicsAspect/Rigidbody2DComponent")]
internal struct Rigidbody2DComponent
{
    public Rigidbody2D Rigidbody2D;
}

[Serializable, ProtoUnityAuthoring("PhysicsAspect/MovementSpeedComponent")]
<<<<<<< HEAD
public struct MovementSpeedComponent
=======
internal struct MovementSpeedComponent
>>>>>>> a41aa0402813acfc4dd4f706c8fadafd701e5387
{
    public float Value;
}