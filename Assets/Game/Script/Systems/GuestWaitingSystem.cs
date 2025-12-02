using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

namespace Game.Script.Systems
{
    public class GuestWaitingSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
    {
        [DI] readonly ProtoWorld _world;
        [DI] readonly PlayerAspect _playerAspect;
        [DI] readonly ViewAspect _viewAspect;
        [DI] readonly BaseAspect _baseAspect;
        [DI] readonly GuestAspect _guestAspect;
        [DI] readonly WorkstationsAspect _workstationsAspect;

        private ProtoItExc _startWaitingTakeOrderIt;
        private ProtoItExc _startWaitingOrderIt;

        public void Init(IProtoSystems systems)
        {
            _startWaitingTakeOrderIt = new(new[]
            {
                typeof(GuestTag), typeof(ReachedTargetPositionEvent)
            }, new[] { typeof(TimerComponent), typeof(WaitingTakeOrderTag), typeof(WaitingOrderTag),
                typeof(GuestServicedTag) });

            _startWaitingOrderIt = new(new[]
            {
                typeof(GuestTag), typeof(WaitingTakeOrderTag), typeof(InteractedEvent)
            }, new[] { typeof(WaitingOrderTag) });

            _startWaitingTakeOrderIt.Init(_world);
            _startWaitingOrderIt.Init(_world);
        }

        public void Run()
        {
            foreach (var guestEntity in _startWaitingTakeOrderIt)
            {
                _guestAspect.WaitingTakeOrderTagPool.Add(guestEntity);
                _playerAspect.InteractablePool.Add(guestEntity);
                ref var timer = ref _baseAspect.TimerPool.Add(guestEntity);
                timer.Duration = 10f;
            }

            foreach (var guestEntity in _startWaitingOrderIt)
            {
                _guestAspect.WaitingOrderTagPool.Add(guestEntity);
                ref var timer = ref _baseAspect.TimerPool.GetOrAdd(guestEntity);
                timer.Elapsed = 0;
                timer.Duration = 20f;
            }
        }

        public void Destroy()
        {
            throw new System.NotImplementedException();
        }
    }
}