// using Leopotam.EcsProto;
// using Leopotam.EcsProto.QoL; // Для AspectInject
// using UnityEngine;
//
// internal class TargetingSystem : IProtoInitSystem, IProtoRunSystem
// {
//     private PhysicsAspect _physicsAspect; 
//
//     private ProtoIt _interactionIterator;
//
//     public void Init(IProtoSystems systems)
//     {
//         ProtoWorld world = systems.World();
//
//         _interactionAspect = (InteractionAspect)world.Aspect(typeof(InteractionAspect));
//         
//         _interactionIterator = new(new[]
//         {
//             typeof(InteractableComponent),
//             typeof(PositionComponent)
//         });
//         _interactionIterator.Im
//     }
//
//     public void Run()
//     {
//        
//     }
// }