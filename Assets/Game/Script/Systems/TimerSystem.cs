using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

public class TimerSystem : IProtoRunSystem, IProtoInitSystem
{
    private ProtoIt _iteratorUpdate;
    [DI] readonly ProtoWorld _world;
    [DI] readonly BaseAspect _aspect;

    public void Init(IProtoSystems systems)
    {
        _iteratorUpdate = new(new[] { typeof(TimerComponent) });
        _iteratorUpdate.Init(_world);
    }

    public void Run()
    {
        float dt = UnityEngine.Time.deltaTime;

        foreach (var timerEntity in _iteratorUpdate)
        {
            ref var timer = ref _aspect.TimerPool.Get(timerEntity);

            if (timer.Completed)
                continue;
        
            timer.Elapsed += dt;

            if (timer.Elapsed >= timer.Duration)
            {
                timer.Completed = true;
            }
        }
    }
}