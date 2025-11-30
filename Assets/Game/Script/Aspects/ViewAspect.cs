using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using UnityEngine;
using UnityEngine.UI;

public class ViewAspect : ProtoAspectInject
{
    public ProtoPool<ProgressBarComponent> ProgressBarPool;
}

[Serializable, ProtoUnityAuthoring("ViewAspect/ProgressBar")]
public struct ProgressBarComponent : IComponent
{
    public Image Image;
    public Color StartColor;
    public Color EndColor;

    public void HideComponent()
    {
        Debug.Log("RFRFRFR");
        Image.enabled = false;
    }

    public void ShowComponent()
    {
        Image.enabled = true;
    }
}