using UnityEngine;
using System.Collections;
using Tools;

public class LayerType : MonoSingleton<LayerType>
{
    public int layerFish
    {
        get { return _layerFish; }
    }

    public int decors
    {
        get { return _decors; }
    }

    [SerializeField] int _layerFish;
    [SerializeField] int _decors;
}
