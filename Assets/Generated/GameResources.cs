using Leopotam.EcsProto.Unity;
using UnityEngine;
using UnityEngine.UIElements;

// This file is auto-generated. Do not modify manually.

public class GameResources
{
    public Visual VisualLink;
    public class Visual
    {
        public Sprite box => Resources.Load<Sprite>("Visual/box");
        public Sprite meat => Resources.Load<Sprite>("Visual/meat");
        public ProtoUnityLink Meat => Resources.Load<ProtoUnityLink>("Visual/Meat");
        public Material New_Material => Resources.Load<Material>("Visual/New Material");
        public Shader Outline => Resources.Load<Shader>("Visual/Outline");
        public Sprite plate => Resources.Load<Sprite>("Visual/plate");
        public ProtoUnityLink Plate => Resources.Load<ProtoUnityLink>("Visual/Plate");
        public Sprite refrigerator => Resources.Load<Sprite>("Visual/refrigerator");
    }

    public GameResources()
    {
        VisualLink = new Visual();
    }
}
