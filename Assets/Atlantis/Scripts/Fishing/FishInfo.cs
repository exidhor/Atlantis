using UnityEngine;
using System;
using Tools;

[Serializable]
public class FishInfo
{
    public FishType type
    {
        get { return _type; }
    }

    public Sprite icon
    {
        get { return _icon; }
    }

    [SerializeField] FishType _type;
    [SerializeField] Sprite _icon;
    [SerializeField] Vector2i _questRange;
    [SerializeField] Vector2i _oneFishPriceRange;
}