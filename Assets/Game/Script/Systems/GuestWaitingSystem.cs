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
    
        private ProtoIt _iterator;
        private ProtoIt _waitingIterator;

        private float _waitingTime = 5f; // #TODO DI
        
        public void Init(IProtoSystems systems)
        {
            var world = systems.World();

            _waitingIterator = new(new[]
            {
                typeof(TimerComponent), typeof(GuestTag)
            });
            _iterator = new(new[]
            {
                typeof(GuestTag), typeof(GuestArrivedEvent)
            });
            _waitingIterator.Init(world);
            _iterator.Init(world);
        }

        public void Run()
        {
            foreach (var entity in _iterator)
            {
                ref var timer = ref _baseAspect.TimerPool.Add(entity);
                timer.Duration = _waitingTime;
                _viewAspect.ProgressBarPool.Get(entity).ShowComponent();
                _guestAspect.GuestArrivedEventPool.DelIfExists(entity);
            }
            foreach (var entity in _waitingIterator)
            {
                ref var timer = ref _baseAspect.TimerPool.Get(entity);

                if (timer.Completed)
                {
                    _viewAspect.ProgressBarPool.Get(entity).HideComponent();
                    _baseAspect.TimerPool.DelIfExists(entity);
                    _guestAspect.GuestLeavingEventPool.Add(entity);
                }
            }
        }

        public void Destroy()
        {
            throw new System.NotImplementedException();
        }
    }
}