using Game.Script.Systems;
using UnityEngine;

namespace Game.Script.Factories
{
    public class GroupGenerationSystemFactory
    {
        private readonly GameObject _guestPrefab;
        public GroupGenerationSystemFactory(GameObject guestPrefab) => this._guestPrefab = guestPrefab;
        
        public GroupGenerationSystem CreateProtoSystem() => new(this._guestPrefab);
    }
}