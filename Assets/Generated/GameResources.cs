using Game.Script;
using Leopotam.EcsProto.Unity;
using UnityEngine;
using UnityEngine.UIElements;

// This file is auto-generated. Do not modify manually.

public class GameResources
{
    public PickableItems PickableItemsLink;
    public class PickableItems
    {
        public PickableItemSO Meat => Resources.Load<PickableItemSO>("PickableItems/Meat");
    }
    public Recipes RecipesLink;
    public class Recipes
    {
        public Recipe meet1 => Resources.Load<Recipe>("Recipes/meet1");
    }
    public Visual VisualLink;
    public class Visual
    {
        public PickableItems PickableItemsLink;
        public class PickableItems
        {
            public Sprite meat => Resources.Load<Sprite>("Visual/PickableItems/meat");
            public Sprite plate => Resources.Load<Sprite>("Visual/PickableItems/plate");
        }
        public Sprite box => Resources.Load<Sprite>("Visual/box");
        public Sprite burner__2_ => Resources.Load<Sprite>("Visual/burner (2)");
        public Shader Outline => Resources.Load<Shader>("Visual/Outline");
        public Material OutlineMat => Resources.Load<Material>("Visual/OutlineMat");
        public Sprite refrigerator => Resources.Load<Sprite>("Visual/refrigerator");
        public Sprite stove => Resources.Load<Sprite>("Visual/stove");
        public Sprite tablet__2_ => Resources.Load<Sprite>("Visual/tablet (2)");
        public Sprite TileMap => Resources.Load<Sprite>("Visual/TileMap");

        public Visual()
        {
            PickableItemsLink = new PickableItems();
        }
    }
    public ProtoUnityAuthoring Fridge => Resources.Load<ProtoUnityAuthoring>("Fridge");
    public CustomAuthoring Guest => Resources.Load<CustomAuthoring>("Guest");
    public CustomAuthoring GuestGroup => Resources.Load<CustomAuthoring>("GuestGroup");
    public CustomAuthoring GuestTable => Resources.Load<CustomAuthoring>("GuestTable");
    public PickableItemsDB Pickable_Items_DB => Resources.Load<PickableItemsDB>("Pickable_Items_DB");
    public ProtoUnityAuthoring Player => Resources.Load<ProtoUnityAuthoring>("Player");
    public RecipesDB Recipes_DB => Resources.Load<RecipesDB>("Recipes_DB");
    public ProtoUnityAuthoring Refrigerator => Resources.Load<ProtoUnityAuthoring>("Refrigerator");
    public ProtoUnityAuthoring Stove => Resources.Load<ProtoUnityAuthoring>("Stove");
    public ProtoUnityAuthoring Table => Resources.Load<ProtoUnityAuthoring>("Table");

    public GameResources()
    {
        PickableItemsLink = new PickableItems();
        RecipesLink = new Recipes();
        VisualLink = new Visual();
    }
}
