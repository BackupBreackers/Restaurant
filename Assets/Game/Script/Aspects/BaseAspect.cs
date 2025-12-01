using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;

public class BaseAspect : ProtoAspectInject
{
    public ProtoPool<TimerComponent> TimerPool;
    public ProtoPool<TimerCompletedTag> TimerCompletedPool;
}

public interface IComponent
{
}

[Serializable]
public struct TimerComponent
{
    public float Elapsed;
    public float Duration;
    public bool Completed;
}

public struct TimerCompletedTag
{
}