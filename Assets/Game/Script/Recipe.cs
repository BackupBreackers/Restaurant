using System;
using Game.Script;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRecipe", menuName = "Game/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeReference, SubclassSelector]
    public PickableItem inputItemType;

    [SerializeReference, SubclassSelector]
    public PickableItem outputItemType;

    [SerializeReference, SubclassSelector]
    public WorkstationItem workstationType;
    public float Duration;
}

[Serializable]
public class WorkstationItem
{
    public Sprite workstationSprite;
}

[Serializable]
public class Stove : WorkstationItem
{
    
}

[Serializable]
public class Fridge : WorkstationItem
{

}

[Serializable]
public class Table : WorkstationItem
{

}

[Serializable]
public class Spawner : WorkstationItem
{

}

[Serializable]
public class FridgeSpawner : Spawner
{

}