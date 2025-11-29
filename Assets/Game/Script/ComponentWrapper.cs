using System;
using UnityEngine;

[Serializable]
public class ComponentWrapper : ScriptableObject {
    [SerializeReference]
    public object value;
}