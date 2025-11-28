using Game.Script;
using Leopotam.EcsProto.Unity;
using UnityEngine;
using UnityEngine.UIElements;

// This file is auto-generated. Do not modify manually.

public class GameResources
{
    public Recipes RecipesLink;
    public class Recipes
    {
        public Recipe meet1 => Resources.Load<Recipe>("Recipes/meet1");
    }
    public Visual VisualLink;
    public class Visual
    {
        public Sprite box => Resources.Load<Sprite>("Visual/box");
        public Sprite meat => Resources.Load<Sprite>("Visual/meat");
        public Shader Outline => Resources.Load<Shader>("Visual/Outline");
        public Material OutlineMat => Resources.Load<Material>("Visual/OutlineMat");
        public Sprite plate => Resources.Load<Sprite>("Visual/plate");
        public Sprite refrigerator => Resources.Load<Sprite>("Visual/refrigerator");
        public Sprite stove => Resources.Load<Sprite>("Visual/stove");
    }
    public ProtoUnityAuthoring Fridge => Resources.Load<ProtoUnityAuthoring>("Fridge");
    public ProtoUnityAuthoring Guest => Resources.Load<ProtoUnityAuthoring>("Guest");
    public ProtoUnityAuthoring Player => Resources.Load<ProtoUnityAuthoring>("Player");
    public RecipesDB Recipes_DB => Resources.Load<RecipesDB>("Recipes_DB");
    public ProtoUnityAuthoring Refrigerator => Resources.Load<ProtoUnityAuthoring>("Refrigerator");
    public ProtoUnityAuthoring Stove => Resources.Load<ProtoUnityAuthoring>("Stove");
    public ProtoUnityAuthoring Table => Resources.Load<ProtoUnityAuthoring>("Table");

    public GameResources()
    {
        RecipesLink = new Recipes();
        VisualLink = new Visual();
    }
}
