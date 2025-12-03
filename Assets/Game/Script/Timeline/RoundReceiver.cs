using System;
using Game.Script.Aspects;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;
using UnityEngine.Playables;

public class RoundReceiver : MonoBehaviour, INotificationReceiver
{
    [SerializeField] private GameObject guestGroup;
    private GuestGroupAspect _guestGroupAspect;
    private ProtoWorld _world;

    public void Start()
    {
        _world = ProtoUnityWorlds.Get();
        _guestGroupAspect = (GuestGroupAspect)_world.Aspect(typeof(GuestGroupAspect));
        
    }

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is GuestGroupSpawnEventMarker spawnMarker)
        {
            var goGroup = Instantiate(guestGroup);
            var auth = goGroup.GetComponent<CustomAuthoring>();

            auth.ProcessAuthoring();
            var entity = auth.Entity();

            entity.TryUnpack(out _, out var protoEntity);
            {
                ref var targetGroupSize = ref _guestGroupAspect.TargetGroupSizePool.Get(protoEntity);
                targetGroupSize.size = spawnMarker.count;
            }
        }
    }
}