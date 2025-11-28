using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace Game.Script.Systems
{
    public class ClearSystem : IProtoInitSystem, IProtoRunSystem
    {
        [DI] readonly ProtoWorld _world;
        [DI] readonly WorkstationsAspect _workstationsAspect;

        private ProtoIt _iteratorPick;
        private ProtoIt _iteratorPlace;
        private ProtoIt _iteratorPickPlace;

        public void Init(IProtoSystems systems)
        {
            _iteratorPick = new(new[] { typeof(ItemPickEvent) });
            _iteratorPlace = new(new[] { typeof(ItemPlaceEvent) });
            _iteratorPickPlace = new(new[] { typeof(PickPlaceEvent) });
            _iteratorPick.Init(_world);
            _iteratorPlace.Init(_world);
            _iteratorPickPlace.Init(_world);
        }

        public void Run()
        {
            foreach (var item in _iteratorPick)
                _workstationsAspect.ItemPickEventPool.DelIfExists(item);

            foreach (var item in _iteratorPlace)
                _workstationsAspect.ItemPlaceEventPool.DelIfExists(item);

            foreach (var item in _iteratorPickPlace)
                _workstationsAspect.ItemPlaceEventPool.DelIfExists(item);
        }
    }
}