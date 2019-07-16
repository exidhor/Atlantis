using UnityEngine;
using System.Collections;
using Tools;

public class LayerType : MonoSingleton<LayerType>
{
    public int layerFish
    {
        get { return _layerFish; }
    }

    [SerializeField] int _layerFish;

}
