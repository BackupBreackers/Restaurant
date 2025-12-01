using System;
using UnityEngine;

namespace Game.Script
{
    [Serializable]
    public class PickableItem
    {
        public Sprite PickupItemSprite;

        public bool Is(Type type)
        {
            return this.GetType() == type;
        }
    }

    [Serializable]
    public class Empty : PickableItem
    {
    }

    [Serializable]
    public class Meat : PickableItem
    {
    }
}