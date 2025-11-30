using System;
using Game.Script;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;

public class PlayerAspect : ProtoAspectInject
{
    public ProtoPool<PlayerInputComponent> InputRawPool;
    public ProtoPool<HealthComponent> HealthPool;

    public ProtoPool<InteractableComponent> InteractablePool;
    public ProtoPool<HolderComponent> HolderPool;
    public ProtoPool<HasItemTag> HasItemTagPool;
}

[Serializable, ProtoUnityAuthoring("PlayerAspect/PlayerInputComponent")]
public struct PlayerInputComponent
{
    public Vector2 MoveDirection;
    private Vector2 LookDirectionOld;

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
    public bool PickPlacePressed;

    public bool RandomSpawnFurniturePressed;

    public bool MoveFurniturePressed;
    public bool IsInMoveState;
}

[Serializable, ProtoUnityAuthoring("PlayerAspect/HealthComponent")]
public struct HealthComponent
{
    public float HealthValue;
}


[Serializable, ProtoUnityAuthoring("PlayerAspect/HolderComponent")]
public struct HolderComponent : IComponent
{ 
    //public ProtoPackedEntityWithWorld Entity;
    public Type ItemType;
    public SpriteRenderer SpriteRenderer;
}

public struct HasItemTag
{
}