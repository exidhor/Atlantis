using UnityEngine;
using System;
using Tools;

[Serializable]
public class CrewInfo
{
    public Crew model
    {
        get { return _model; }
    }

    public Sprite icon
    {
        get { return _icon; }
    }

    public Color frontColor
    {
        get { return _frontColor; }
    }

    public Color backgroundColor
    {
        get { return _backgroundColor; }
    }

    [SerializeField] Crew _model;

    [Header("UIs")]
    [SerializeField] Sprite _icon;
    [SerializeField] Color _frontColor;
    [SerializeField] Color _backgroundColor;

    [Header("Game Logic")]
    [SerializeField] Vector2i _priceRange;

    public int GetRandomPrice()
    {
        return UnityEngine.Random.Range(_priceRange.x, _priceRange.y);
    }
}
