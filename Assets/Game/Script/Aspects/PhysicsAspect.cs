using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;

internal class PhysicsAspect : ProtoAspectInject
{
    public ProtoPool<PositionComponent> PositionPool;
    public ProtoPool<Rigidbody2DComponent> Rigidbody2DPool;
}

[Serializable, ProtoUnityAuthoring("PhysicsAspect/PositionComponent")]
internal struct PositionComponent
{
    public Vector2 Position;
}

[Serializable, ProtoUnityAuthoring("PhysicsAspect/Rigidbody2DComponent")]
internal struct Rigidbody2DComponent
{
    public Rigidbody2D Rigidbody2D;
}