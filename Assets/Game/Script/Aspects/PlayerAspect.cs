using System;
using Game.Script;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;

public class PlayerAspect : ProtoAspectInject
{
    public ProtoPool<PlayerInputComponent> InputRawPool;
    public ProtoPool<InteractableComponent> InteractablePool;
    public ProtoPool<HolderComponent> HolderPool;
    public ProtoPool<HasItemTag> HasItemTagPool;
}

[Serializable]
public struct HolderComponent : IComponent
{
    public Type Item;
    public SpriteRenderer SpriteRenderer;

    public void Clear()
    {
        Item = null;
        SpriteRenderer = null;
    }
}

public struct HasItemTag
{
}

[Serializable]
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