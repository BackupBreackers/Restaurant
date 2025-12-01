using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

public class EndGameSystem : IProtoInitSystem, IProtoRunSystem
{
    [DI] ProtoWorld _world;
    private ProtoIt _it;
    private ProtoIt _it2;

    public void Init(IProtoSystems systems)
    {
        _it = new(new[] { typeof(WaitingOrderTag), typeof(TimerCompletedTag)});
        _it2 = new(new[] { typeof(WaitingTakeOrderTag), typeof(TimerCompletedTag)});
        _it.Init(_world);
        _it2.Init(_world);
    }

    public void Run()
    {
        foreach (var guest in _it)
        {
            Debug.LogError("ПРОЕБАЛИ ожидание заказа");
            break;
        }

        foreach (var guest in _it2)
        {
            Debug.LogError("ПРОЕБАЛИ не взял заказаз, сука тварь");
            break;
        }
    }
}