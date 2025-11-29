using System;
using UnityEngine;

namespace Game.Script
{
    [Serializable]
    public abstract class PickableItem : ScriptableObject
    {
        public Sprite PickupItemSprite;
    }

    [Serializable]
    public class Meat : PickableItem { }
}