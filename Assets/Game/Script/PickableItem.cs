using System;
using UnityEngine;

namespace Game.Script
{
    [Serializable]
    public class PickableItem
    {
        public Sprite PickupItemSprite;
    }
    
    [Serializable]
    public class Empty : PickableItem { }

    [Serializable]
    public class Meat : PickableItem { }
}