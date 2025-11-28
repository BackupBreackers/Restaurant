using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace Game.Script.Systems
{
    public class ProgressBarSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoIt _iterator;
        [DI] readonly ProtoWorld _world;
        [DI] readonly WorkstationsAspect _workstationsAspect;
        [DI] readonly ViewAspect _viewAspect;
        [DI] readonly BaseAspect _baseAspect;
        
        public void Init(IProtoSystems systems)
        {
            _iterator = new(new[] { typeof(TimerComponent), typeof(ProgressBarComponent)} );
            _iterator.Init(_world);
        }

        public void Run()
        {
            foreach (var progressBarEntity in _iterator)
            {
                ref var progressBar = ref _viewAspect.ProgressBarPool.Get(progressBarEntity);
                ref var timer = ref _baseAspect.TimerPool.Get(progressBarEntity);
                progressBar.Image.fillAmount = timer.Elapsed/timer.Duration;
            }
        }
    }

}