using Game.Script;
using Leopotam.EcsProto.Unity;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;
using UnityEngine.UIElements;

// This file is auto-generated. Do not modify manually.

public class GameResources
{
    public LevelConfigs LevelConfigsLink;
    public class LevelConfigs
    {
    }
    public PickableItems PickableItemsLink;
    public class PickableItems
    {
        public PickableItemSO Fish0 => Resources.Load<PickableItemSO>("PickableItems/Fish0");
        public PickableItemSO Fish1 => Resources.Load<PickableItemSO>("PickableItems/Fish1");
        public PickableItemSO Fish2 => Resources.Load<PickableItemSO>("PickableItems/Fish2");
        public PickableItemSO Fish3 => Resources.Load<PickableItemSO>("PickableItems/Fish3");
        public PickableItemSO Meat => Resources.Load<PickableItemSO>("PickableItems/Meat");
    }
    public PlacementObjects PlacementObjectsLink;
    public class PlacementObjects
    {
        public PlacementObject Fridge => Resources.Load<PlacementObject>("PlacementObjects/Fridge");
        public PlacementObject FridgeSpawner => Resources.Load<PlacementObject>("PlacementObjects/FridgeSpawner");
        public PlacementObject Stove => Resources.Load<PlacementObject>("PlacementObjects/Stove");
        public PlacementObject Table => Resources.Load<PlacementObject>("PlacementObjects/Table");
    }
    public Recipes RecipesLink;
    public class Recipes
    {
        public Recipe fish0_fish1 => Resources.Load<Recipe>("Recipes/fish0-fish1");
        public Recipe meet1 => Resources.Load<Recipe>("Recipes/meet1");
    }
    public UIPrefabs UIPrefabsLink;
    public class UIPrefabs
    {
        public CanvasScaler HUD => Resources.Load<CanvasScaler>("UIPrefabs/HUD");
    }
    public Visual VisualLink;
    public class Visual
    {
        public Animatiion AnimatiionLink;
        public class Animatiion
        {
        }
        public Materials MaterialsLink;
        public class Materials
        {
            public Material Group_140 => Resources.Load<Material>("Visual/Materials/Group 140");
        }
        public PickableItems PickableItemsLink;
        public class PickableItems
        {
            public Sprite FishStates => Resources.Load<Sprite>("Visual/PickableItems/FishStates");
            public Sprite meat => Resources.Load<Sprite>("Visual/PickableItems/meat");
            public Sprite plate => Resources.Load<Sprite>("Visual/PickableItems/plate");
        }
        public Prefab PrefabLink;
        public class Prefab
        {
            public SpriteSkin MouseCook => Resources.Load<SpriteSkin>("Visual/Prefab/MouseCook");
            public SpriteSkin MouseWithItsBack => Resources.Load<SpriteSkin>("Visual/Prefab/MouseWithItsBack");
            public SpriteSkin MouseWithLeft => Resources.Load<SpriteSkin>("Visual/Prefab/MouseWithLeft");
        }
        public UI UILink;
        public class UI
        {
            public Sprite Birds => Resources.Load<Sprite>("Visual/UI/Birds");
            public Sprite CastleDown => Resources.Load<Sprite>("Visual/UI/CastleDown");
            public Sprite CastleUP => Resources.Load<Sprite>("Visual/UI/CastleUP");
            public Sprite MenuBackground => Resources.Load<Sprite>("Visual/UI/MenuBackground");
            public Sprite miceahoy => Resources.Load<Sprite>("Visual/UI/miceahoy");
            public Sprite PauseMenuBack => Resources.Load<Sprite>("Visual/UI/PauseMenuBack");
            public Sprite ship => Resources.Load<Sprite>("Visual/UI/ship");
            public Sprite SuperHint => Resources.Load<Sprite>("Visual/UI/SuperHint");
            public Sprite Сloud1 => Resources.Load<Sprite>("Visual/UI/Сloud1");
            public Sprite Сloud2 => Resources.Load<Sprite>("Visual/UI/Сloud2");
            public Sprite Сloud3 => Resources.Load<Sprite>("Visual/UI/Сloud3");
        }
        public Sprite barrel => Resources.Load<Sprite>("Visual/barrel");
        public Sprite box => Resources.Load<Sprite>("Visual/box");
        public Sprite burner => Resources.Load<Sprite>("Visual/burner");
        public Sprite dirtyPlates => Resources.Load<Sprite>("Visual/dirtyPlates");
        public Sprite fridge => Resources.Load<Sprite>("Visual/fridge");
        public Sprite Group_140 => Resources.Load<Sprite>("Visual/Group 140");
        public Sprite GuestBackIsTurned => Resources.Load<Sprite>("Visual/GuestBackIsTurned");
        public Sprite GuestIsSittingInFrontOf => Resources.Load<Sprite>("Visual/GuestIsSittingInFrontOf");
        public Sprite GuestIsSittingSideways => Resources.Load<Sprite>("Visual/GuestIsSittingSideways");
        public Sprite GuestIsSittingWithHisBack => Resources.Load<Sprite>("Visual/GuestIsSittingWithHisBack");
        public Sprite guestsTable => Resources.Load<Sprite>("Visual/guestsTable");
        public Sprite GuestStandsSideways => Resources.Load<Sprite>("Visual/GuestStandsSideways");
        public Sprite MouseCook => Resources.Load<Sprite>("Visual/MouseCook");
        public Sprite MouseWithItsBack => Resources.Load<Sprite>("Visual/MouseWithItsBack");
        public Sprite MouseWithLeft => Resources.Load<Sprite>("Visual/MouseWithLeft");
        public Shader Outline => Resources.Load<Shader>("Visual/Outline");
        public Material OutlineMat => Resources.Load<Material>("Visual/OutlineMat");
        public Sprite plate => Resources.Load<Sprite>("Visual/plate");
        public Sprite refrigerator => Resources.Load<Sprite>("Visual/refrigerator");
        public Sprite sidePlate => Resources.Load<Sprite>("Visual/sidePlate");
        public Sprite sink => Resources.Load<Sprite>("Visual/sink");
        public Sprite table => Resources.Load<Sprite>("Visual/table");
        public Sprite TableWithPlates => Resources.Load<Sprite>("Visual/TableWithPlates");
        public Sprite TileMap => Resources.Load<Sprite>("Visual/TileMap");

        public Visual()
        {
            AnimatiionLink = new Animatiion();
            MaterialsLink = new Materials();
            PickableItemsLink = new PickableItems();
            PrefabLink = new Prefab();
            UILink = new UI();
        }
    }
    public ProtoUnityAuthoring Fridge => Resources.Load<ProtoUnityAuthoring>("Fridge");
    public CustomAuthoring FridgeSpawner => Resources.Load<CustomAuthoring>("FridgeSpawner");
    public CustomAuthoring Guest => Resources.Load<CustomAuthoring>("Guest");
    public CustomAuthoring GuestGroup => Resources.Load<CustomAuthoring>("GuestGroup");
    public CustomAuthoring GuestTable => Resources.Load<CustomAuthoring>("GuestTable");
    public PickableItemsDB Pickable_Items_DB => Resources.Load<PickableItemsDB>("Pickable_Items_DB");
    public PivotToRealPositionDifferences PivotToRealPositionDifferences => Resources.Load<PivotToRealPositionDifferences>("PivotToRealPositionDifferences");
    public PlacementObjectsDB PlacementObjects_DB => Resources.Load<PlacementObjectsDB>("PlacementObjects_DB");
    public CustomAuthoring Player => Resources.Load<CustomAuthoring>("Player");
    public RecipesDB Recipes_DB => Resources.Load<RecipesDB>("Recipes_DB");
    public CustomAuthoring Refrigerator => Resources.Load<CustomAuthoring>("Refrigerator");
    public ProtoUnityAuthoring Stove => Resources.Load<ProtoUnityAuthoring>("Stove");
    public ProtoUnityAuthoring Table => Resources.Load<ProtoUnityAuthoring>("Table");

    public GameResources()
    {
        LevelConfigsLink = new LevelConfigs();
        PickableItemsLink = new PickableItems();
        PlacementObjectsLink = new PlacementObjects();
        RecipesLink = new Recipes();
        UIPrefabsLink = new UIPrefabs();
        VisualLink = new Visual();
    }
}
