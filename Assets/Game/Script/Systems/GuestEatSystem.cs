// using Game.Script.Aspects;
// using Leopotam.EcsProto;
// using Leopotam.EcsProto.QoL;
// using UnityEngine;
//
// namespace Game.Script.Systems
// {
//     public class GuestEatSystem : IProtoInitSystem, IProtoRunSystem
//     {
//         [DI] readonly PlayerAspect _playerAspect;
//         [DI] readonly GuestAspect _guestAspect;
//
//         private ProtoIt _orderIterator;
//         private ProtoIt _playerIterator;
//         
//         public void Init(IProtoSystems systems)
//         {
//             var world = systems.World();
//             _orderIterator = new(new[]
//             {
//                 typeof(GuestTag), typeof(InteractableComponent),
//                 typeof(DidGotOrder),
//             });
//             _orderIterator.Init(world);
//         }
//
//         public void Run()
//         {
//             foreach (var entity in _orderIterator)
//             {
//                 ref var holder = ref _playerAspect.HolderPool.Get(entity);
//
//                 if (holder.ItemType is null)
//                 {
//                     Debug.Log("Зачем ты кормишь воздухом?");
//                     _guestAspect.PickPlaceEventPool.DelIfExists(entity);
//                     continue;
//                 }
//
//                 Debug.Log("ВСё, поел, пака");
//                 _guestAspect.GuestLeavingEventPool.Add(entity);
//             }
//         }
//     }
// }