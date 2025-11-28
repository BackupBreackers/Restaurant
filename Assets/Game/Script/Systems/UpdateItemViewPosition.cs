// using Leopotam.EcsProto;
// using Leopotam.EcsProto.QoL;
//
// public class UpdateItemViewPosition : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
// {
//     private ProtoIt _iterator;
//     [DI] readonly WorkstationsAspect _workstationsAspect;
//     [DI] readonly PlayerAspect _playerAspect;
//     [DI] readonly PhysicsAspect _physicsAspect;
//
//     public void Init(IProtoSystems systems)
//     {
//         var w = systems.World();
//         _iterator = new ProtoIt(new[] { typeof(PositionComponent), typeof(HolderComponent), typeof(HasItemTag) });
//         _iterator.Init(w);
//     }
//
//     public void Run()
//     {
//         foreach (var holderEntity in _iterator)
//         {
//             ref var holder = ref _playerAspect.HolderPool.Get(holderEntity);
//             ref var pos = ref _physicsAspect.PositionPool.Get(holderEntity);
//
//         }
//     }
//
//     public void Destroy()
//     {
//     }
// }