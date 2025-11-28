using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Game/Recipe")]
public class Recipe : ScriptableObject
{
    public PickupItemType inputItemType;
    public WorkstationsType workstationType;
    public PickupItemType outputItemType;
    public Sprite outputItemSprite;
    public float Duration;
}