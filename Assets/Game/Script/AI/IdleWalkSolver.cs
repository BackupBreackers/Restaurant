// using Game.Script.Aspects;
// using Leopotam.EcsProto;
// using Leopotam.EcsProto.Ai.Utility;
// using Leopotam.EcsProto.QoL;
// using UnityEngine;
//
// namespace Game.Script.AI
// {
//     public class IdleWalkSolver : IAiUtilitySolver
//     {
//         [DI] private readonly GuestAspect _guest = default;
//         [DI] private readonly PhysicsAspect _physics = default;
//
//         public float Solve(ProtoEntity entity)
//         {
//             ref TargetPositionComponent target = ref _guest.TargetPositionComponentPool.Get(entity);
//             ref PositionComponent position = ref _guest.PositionComponentPool.Get(entity);
//
//             if (Vector2.Distance(position.Position, target.Position) > 0.5f) return 1f;
//             return 0f;
//         }
//
//         public void Apply(ProtoEntity entity)
//         {
//             ref PositionComponent position = ref _guest.PositionComponentPool.Get(entity);
//             ref MovementSpeedComponent movementSpeed = ref _guest.MovementSpeedComponentPool.Get(entity);
//             ref TargetPositionComponent target = ref _guest.TargetPositionComponentPool.Get(entity);
//             var direction = (target.Position - position.Position).normalized;
//             ref Rigidbody2DComponent rb = ref _physics.Rigidbody2DPool.Get(entity);
//             rb.Rigidbody2D.linearVelocity = direction * movementSpeed.Value;
//         }
//     }
// }
