using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

namespace Game.Script.Systems
{
    public class GuestWaitingSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
    {
        [DI] readonly PlayerAspect _playerAspect;
        [DI] readonly ViewAspect _viewAspect;
        [DI] readonly BaseAspect _baseAspect;
        [DI] readonly GuestAspect _guestAspect;
        [DI] readonly WorkstationsAspect _workstationsAspect;
    
        private ProtoIt _startWaitingIterator;
        private ProtoIt _waitingIterator;
        private ProtoIt _playerInteractedIterator;

        private float _waitingTime = 5f; // #TODO DI
        private float _orderWaitingTime = 15f; // #TODO DI
        
        public void Init(IProtoSystems systems)
        {
            var world = systems.World();

            _waitingIterator = new(new[]
            {
                typeof(TimerComponent), typeof(GuestTag)
            });
            _startWaitingIterator = new(new[]
            {
                typeof(GuestTag), typeof(GuestArrivedEvent)
            });
            _playerInteractedIterator = new ProtoIt(new[]
            {
                typeof(GuestTag), typeof(GuestTakeOrderEvent), typeof(TimerComponent)
            });
            _waitingIterator.Init(world);
            _startWaitingIterator.Init(world);
            _playerInteractedIterator.Init(world);
        }

        public void Run()
        {
            foreach (var entity in _startWaitingIterator)
            {
                ref var timer = ref _baseAspect.TimerPool.Add(entity);
                timer.Duration = _waitingTime;
                _viewAspect.ProgressBarPool.Get(entity).ShowComponent();
                _guestAspect.GuestArrivedEventPool.DelIfExists(entity);
                _guestAspect.InteractableComponentPool.Add(entity);
                _guestAspect.GuestIsWalkingPool.DelIfExists(entity);
                ref var packedTable = ref _guestAspect.TargetPositionComponentPool.Get(entity).Table;
                if (!packedTable.TryUnpack(out var world, out var protoEntity))
                {
                    Debug.Log("столик потерялся по дороге");
                    continue;
                }

                ref var table = ref _workstationsAspect.GuestTablePool.GetOrAdd(protoEntity);
                if (table.Guests is null) table.Guests = new();
                table.Guests.Add(world.PackEntityWithWorld(entity));
            }
            foreach (var entity in _playerInteractedIterator)
            {
                var interacted = _guestAspect.DidPlayerInteractedPool.Has(entity);
                if (interacted) continue;
                ref var timer = ref _baseAspect.TimerPool.Get(entity);
                timer.Elapsed = 0;
                timer.Duration = _orderWaitingTime;
                _guestAspect.GuestArrivedEventPool.DelIfExists(entity);
                _guestAspect.DidPlayerInteractedPool.Add(entity);
            }
            foreach (var entity in _waitingIterator)
            {
                ref var timer = ref _baseAspect.TimerPool.Get(entity);

                if (timer.Completed)
                {
                    _viewAspect.ProgressBarPool.Get(entity).HideComponent();
                    _baseAspect.TimerPool.DelIfExists(entity);
                    _guestAspect.GuestLeavingEventPool.Add(entity);
                    _guestAspect.GuestIsWalkingPool.Add(entity);
                }
            }
        }

        public void Destroy()
        {
            throw new System.NotImplementedException();
        }
    }
}