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

    public Color holdFrontColor
    {
        get { return _holdFrontColor; }
    }

    public Color holdBackgroundColor
    {
        get { return _holdBackgroundColor; }
    }

    [Header("Game Design Values")]
    [SerializeField] FishType _type;
    [SerializeField] Vector2i _questRange;
    [SerializeField] Vector2i _oneFishPriceRange;

    [Header("UIs")]
    [SerializeField] Sprite _icon;
    [SerializeField] Color _holdFrontColor;
    [SerializeField] Color _holdBackgroundColor;

    public int GetRandomCount()
    {
        return UnityEngine.Random.Range(_questRange.x, _questRange.y);
    }

    public int GetRandomPrice()
    {
        return UnityEngine.Random.Range(_oneFishPriceRange.x, _oneFishPriceRange.y);
    }
}