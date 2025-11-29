using System;
using Game.Script;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRecipe", menuName = "Game/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeReference, SubclassSelector]
    public SerializableType inputItemType;

    [SerializeReference, SubclassSelector]
    public SerializableType outputItemType;

    [SerializeReference, SubclassSelector]
    public WorkstationItem workstationType;
    public float Duration;
}


public class WorkstationItem : ScriptableObject
{
    
}


public class Stove : WorkstationItem
{
    
}