using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;

class PlayerAspect : ProtoAspectInject
{
    public ProtoPool<PlayerInputComponent> InputRawPool;
    public ProtoPool<HealthComponent> HealthPool;
    
    public ProtoPool<InteractableComponent> InteractablePool;
    public ProtoPool<HolderComponent> HolderPool;
    public ProtoPool<HasItemTag> HasItemTagPool;
}

[Serializable, ProtoUnityAuthoring("PlayerAspect/PlayerInputComponent")]
internal struct PlayerInputComponent
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
}

[Serializable, ProtoUnityAuthoring("PlayerAspect/HealthComponent")]
internal struct HealthComponent
{
    public float HealthValue;
}

[Serializable, ProtoUnityAuthoring("PlayerAspect/InteractableComponent")]
internal struct InteractableComponent
{
    public SpriteOutlineController OutlineController;
}

[Serializable, ProtoUnityAuthoring("PlayerAspect/HolderComponent")]
internal struct HolderComponent
{
    public ProtoPackedEntityWithWorld Entity;
    public Vector2 ItemPosition;
}

internal struct HasItemTag
{
    
}