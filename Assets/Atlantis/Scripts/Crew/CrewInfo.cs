using UnityEngine;
using System;
using Tools;

[Serializable]
public class CrewInfo
{
    public string name
    {
        get { return _name; }
    }

    public Crew model
    {
        get { return _model; }
    }

    public Sprite icon
    {
        get { return _icon; }
    }

    public Texture flagIcon
    {
        get { return _flagIcon; }
    }

    public Color frontColor
    {
        get { return _frontColor; }
    }

    public Color backgroundColor
    {
        get { return _backgroundColor; }
    }

    public float rate
    {
        get { return _rate; }
    }

    [SerializeField] string _name;
    [SerializeField] Crew _model;

    [Header("UIs")]
    [SerializeField] Sprite _icon;
    [SerializeField] Texture _flagIcon;
    [SerializeField] Color _frontColor;
    [SerializeField] Color _backgroundColor;

    [Header("Game Logic")]
    [SerializeField] Vector2i _priceRange;
    [SerializeField, Range(0f, 1f)] float _rate; 

    public int GetRandomPrice()
    {
        return UnityEngine.Random.Range(_priceRange.x, _priceRange.y);
    }
}
