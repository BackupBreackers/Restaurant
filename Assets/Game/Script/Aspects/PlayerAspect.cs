using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;

class PlayerAspect : ProtoAspectInject
{
    public ProtoPool<PlayerInputComponent> InputRawPool;
    public ProtoPool<HealthComponent> HealthPool;
    public ProtoPool<MovementSpeedComponent> SpeedPool;
}

[Serializable, ProtoUnityAuthoring("PlayerAspect/PlayerInputComponent")]
internal struct PlayerInputComponent
{
    public Vector2 MoveDirection;
    public Vector2 LookDirectionOld;
    public Vector2 LookDirection
    {
        get
        {
            if (MoveDirection != Vector2.zero)
            {
                LookDirectionOld = MoveDirection.normalized;
                return MoveDirection.normalized;
            }
            return LookDirectionOld;
        }
    }

    
    public bool InteractPressed;
}

[Serializable, ProtoUnityAuthoring("PlayerAspect/MovementSpeedComponent")]
internal struct MovementSpeedComponent
{
    public float Value;
}

[Serializable, ProtoUnityAuthoring("PlayerAspect/HealthComponent")]
internal struct HealthComponent
{
    public float HealthValue;
}