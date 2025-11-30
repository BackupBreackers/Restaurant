using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;
using UnityEngine.InputSystem;

internal class PlayerTargetSystem : IProtoInitSystem, IProtoRunSystem
{
    private ProtoIt _iteratorInteractable;
    private ProtoIt _iteratorPlayer;
    private ProtoIt _iteratorGuest;
    private PlayerAspect _playerAspect;
    private PhysicsAspect _physicsAspect;
    private PlacementAspect _placementAspect;
    private WorkstationsAspect _workstationsAspect;
    private GuestAspect _guestAspect;
    private ProtoWorld _world;

    public void Init(IProtoSystems systems)
    {
        _world = systems.World();
        _playerAspect = (PlayerAspect)_world.Aspect(typeof(PlayerAspect));
        _physicsAspect = (PhysicsAspect)_world.Aspect(typeof(PhysicsAspect));
        _workstationsAspect = (WorkstationsAspect)_world.Aspect(typeof(WorkstationsAspect));
        _placementAspect = (PlacementAspect)_world.Aspect(typeof(PlacementAspect));
        _guestAspect = (GuestAspect)_world.Aspect(typeof(GuestAspect));
        _iteratorInteractable = new(new[] { typeof(InteractableComponent), typeof(PositionComponent)});
        _iteratorPlayer = new(new[] { typeof(PlayerInputComponent), typeof(PositionComponent) });
        _iteratorGuest = new (new[] {typeof(GuestTag), typeof(InteractableComponent)});
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
                ref InteractableComponent interactable = ref _playerAspect.InteractablePool.Get(entityInteractable);
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
                            interactableComponent = interactable;
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
                ref InteractableComponent guestInteractable = ref _guestAspect.InteractableComponentPool.Get(entityGuest);

                float distance = Vector2.Distance(guestPosition.Position, playerPosition.Position);
                if (distance < range)
                {
                    var vector = guestPosition.Position - playerPosition.Position;
                    var angle = Vector2.SignedAngle(vector, playerInput.LookDirection);
                    float absAngle = Mathf.Abs(angle);

                    if (absAngle < 60)
                    {
                        // подсветка гостя
                        // guestInteractable.OutlineController.SetHighlight(true);

                        if (playerInput.InteractPressed)
                        {
                            if (!_guestAspect.GuestTakeOrderEventPool.Has(entityGuest))
                            {
                                _guestAspect.GuestTakeOrderEventPool.Add(entityGuest);
                            }
                        }
                    }
                }
            }
        }
    }
}