using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

internal class PlayerTargetSystem : IProtoInitSystem, IProtoRunSystem
{
    private ProtoIt _iteratorInteractable;
    private ProtoIt _iteratorPlayer;
    private PlayerAspect _playerAspect;
    private PhysicsAspect _physicsAspect;
    private WorkstationsAspect _workstationsAspect;
    private ProtoWorld _world;

    public void Init(IProtoSystems systems)
    {
        _world = systems.World();
        _playerAspect = (PlayerAspect)_world.Aspect(typeof(PlayerAspect));
        _physicsAspect = (PhysicsAspect)_world.Aspect(typeof(PhysicsAspect));
        _workstationsAspect = (WorkstationsAspect)_world.Aspect(typeof(WorkstationsAspect));

        _iteratorInteractable = new(new[] { typeof(InteractableComponent), typeof(PositionComponent) });
        _iteratorPlayer = new(new[] { typeof(PlayerInputComponent), typeof(PositionComponent) });

        _iteratorInteractable.Init(_world);
        _iteratorPlayer.Init(_world);
    }

    public void Run()
    {
        foreach (var entityPlayer in _iteratorPlayer)
        {
            var range = 5f;


            ref var playerPosition = ref _physicsAspect.PositionPool.Get(entityPlayer);
            ref var playerInput = ref _playerAspect.InputRawPool.Get(entityPlayer);

            InteractableComponent interactableComponent = default;
            ProtoEntity targetEntity = default;
            var minAngle = float.MaxValue;

            foreach (var entityInteractable in _iteratorInteractable)
            {
                ref InteractableComponent interactable = ref _playerAspect.InteractablePool.Get(entityInteractable);
                ref PositionComponent interactablePosition = ref _physicsAspect.PositionPool.Get(entityInteractable);

                interactable.OutlineController.SetHighlight(false);

                if (Vector2.Distance(interactablePosition.Position, playerPosition.Position) < range)
                {
                    var vector = interactablePosition.Position - playerPosition.Position;

                    var angle = Vector2.SignedAngle(vector, playerInput.LookDirection);
                    float absAngle = Mathf.Abs(angle);

                    if (absAngle < 45)
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
                interactableComponent.OutlineController.SetHighlight(true);
                
                if(!playerInput.InteractPressed) continue;
                
                if (!_workstationsAspect.InteractedEventPool.Has(targetEntity))
                {
                    _workstationsAspect.InteractedEventPool.Add(targetEntity);
                    ref InteractedEvent r = ref _workstationsAspect.InteractedEventPool.Get(targetEntity);
                    r.Invoker = _world.PackEntityWithWorld(entityPlayer);
                }
            }
        }
    }
}