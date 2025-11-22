using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;
using Object = UnityEngine.Object;

public class UpdateItemViewPosition : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    private ProtoIt _iterator;
    [DI] readonly WorkstationsAspect _workstationsAspect;
    [DI] readonly PlayerAspect _playerAspect;
    [DI] readonly PhysicsAspect _physicsAspect;

    public void Init(IProtoSystems systems)
    {
        var w = systems.World();
        _iterator = new ProtoIt(new[] { typeof(PositionComponent), typeof(HolderComponent), typeof(HasItemTag) });
        _iterator.Init(w);
    }

    public void Run()
    {
        foreach (var holderEntity in _iterator)
        {
            ref var holder = ref _playerAspect.HolderPool.Get(holderEntity);
            ref var pos = ref _physicsAspect.PositionPool.Get(holderEntity);

            if (holder.Entity.TryUnpack(out _, out var item))
            {
                ref var viewC = ref _workstationsAspect.ViewPool.Get(item);
                viewC.GameObjectTransform.transform.position = holder.ItemPosition + pos.Position;
            }
        }
    }

    public void Destroy()
    {
    }
}