using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace Game.Script.Systems
{
    public class ProgressBarSystem : IProtoInitSystem, IProtoRunSystem
    {
        [DI] readonly ProtoWorld _world;
        [DI] readonly WorkstationsAspect _workstationsAspect;
        [DI] readonly ViewAspect _viewAspect;
        [DI] readonly BaseAspect _baseAspect;

        private ProtoIt _iterator;
        private ProtoIt _iteratorDel;

        public void Init(IProtoSystems systems)
        {
            _iterator = new(new[] { typeof(TimerComponent), typeof(ProgressBarComponent) });
            _iteratorDel = new(new[] { typeof(TimerCompletedTag), typeof(ProgressBarComponent) });
            _iterator.Init(_world);
            _iteratorDel.Init(_world);
        }

        public void Run()
        {
            foreach (var progressBarEntity in _iterator)
            {
                ref var progressBar = ref _viewAspect.ProgressBarPool.Get(progressBarEntity);
                ref var timer = ref _baseAspect.TimerPool.Get(progressBarEntity);
                if (progressBar.IsActive)
                    progressBar.Image.fillAmount = timer.Elapsed / timer.Duration;
                else
                    progressBar.ShowComponent();
            }

            foreach (var progressBarEntity in _iteratorDel)
            {
                ref var progressBar = ref _viewAspect.ProgressBarPool.Get(progressBarEntity);
                progressBar.HideComponent();
            }
        }
    }
}