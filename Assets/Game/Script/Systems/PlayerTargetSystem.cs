using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

internal class PlayerTargetSystem : IProtoInitSystem, IProtoRunSystem
{
    [DI] private PlayerAspect _playerAspect;
    [DI] private PhysicsAspect _physicsAspect;
    [DI] private PlacementAspect _placementAspect;
    [DI] private WorkstationsAspect _workstationsAspect;
    [DI] private GuestAspect _guestAspect;

    private ProtoIt _iteratorInteractable;
    private ProtoIt _iteratorPlayer;
    private ProtoIt _iteratorGuest;
    private ProtoWorld _world;

    public void Init(IProtoSystems systems)
    {
        _world = systems.World();

        _iteratorInteractable = new(new[] { typeof(InteractableComponent), typeof(PositionComponent) });
        _iteratorPlayer = new(new[] { typeof(PlayerInputComponent), typeof(PositionComponent) });
        _iteratorGuest = new(new[] { typeof(GuestTag), typeof(InteractableComponent) });

        _iteratorInteractable.Init(_world);
        _iteratorPlayer.Init(_world);
        _iteratorGuest.Init(_world);
    }

    public void Run()
    {
        foreach (var entityPlayer in _iteratorPlayer)
        {
            var range = 5f;

            ref var playerPosition = ref _physicsAspect.PositionPool.Get(entityPlayer);
            ref var playerInput = ref _playerAspect.InputRawPool.Get(entityPlayer);

            //если игрок сейчас двигает мебель, то что-то подсвечивать не нужно
            //if (playerInput.IsInMoveState) continue;

            InteractableComponent interactableComponent = default;
            ProtoEntity targetEntity = default;
            var minAngle = float.MaxValue;

            foreach (var entityInteractable in _iteratorInteractable)
            {
                //ref InteractableComponent interactable = ref _playerAspect.InteractablePool.Get(entityInteractable);
                ref PositionComponent interactablePosition = ref _physicsAspect.PositionPool.Get(entityInteractable);

                //interactable.OutlineController.SetHighlight(false);

                if (Vector2.Distance(interactablePosition.Position, playerPosition.Position) < range)
                {
                    var vector = interactablePosition.Position - playerPosition.Position;

                    var angle = Vector2.SignedAngle(vector, playerInput.LookDirection);
                    float absAngle = Mathf.Abs(angle);

                    if (absAngle < 60)
                    {
                        if (absAngle < minAngle)
                        {
                            minAngle = absAngle;
                            //interactableComponent = interactable;
                            targetEntity = entityInteractable;
                        }
                    }
                }
            }

            if (minAngle != float.MaxValue)
            {
                //interactableComponent.OutlineController.SetHighlight(true);

                if (playerInput.InteractPressed)
                {
                    if (!_workstationsAspect.PickPlaceEventPool.Has(targetEntity))
                    {
                        _workstationsAspect.PickPlaceEventPool.Add(targetEntity);
                        ref PickPlaceEvent r = ref _workstationsAspect.PickPlaceEventPool.Get(targetEntity);
                        r.Invoker = _world.PackEntityWithWorld(entityPlayer);
                    }
                }
                else if (playerInput.MoveFurniturePressed)
                {
                    if (!_placementAspect.MoveStatePool.Has(targetEntity))
                    {
                        _placementAspect.MoveStatePool.Add(targetEntity);
                        ref var m = ref _placementAspect.MoveStatePool.Get(targetEntity);
                        m.Invoker = _world.PackEntityWithWorld(entityPlayer);
                    }
                }
            }

            foreach (var entityGuest in _iteratorGuest)
            {
                ref PositionComponent guestPosition = ref _physicsAspect.PositionPool.Get(entityGuest);
                //ref InteractableComponent guestInteractable = ref _guestAspect.InteractableComponentPool.Get(entityGuest);

                var distance = Vector2.Distance(guestPosition.Position, playerPosition.Position);
                if (!(distance < range)) continue;
                var vector = guestPosition.Position - playerPosition.Position;
                var angle = Vector2.SignedAngle(vector, playerInput.LookDirection);
                var absAngle = Mathf.Abs(angle);

                if (!(absAngle < 60)) continue;
                // guestInteractable.OutlineController.SetHighlight(true);

                if (!playerInput.InteractPressed) continue;
                
                if (!_workstationsAspect.InteractedEventPool.Has(entityGuest))
                    _workstationsAspect.InteractedEventPool.Add(entityGuest);
            }
        }
    }
}