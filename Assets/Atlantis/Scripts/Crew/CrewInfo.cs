using UnityEngine;
using System;

[Serializable]
public class CrewInfo
{
    public Crew model
    {
        get { return _model; }
    }

    [SerializeField] Crew _model;

    [Header("UIs")]
    [SerializeField] Sprite _icon;
    [SerializeField] Color _holdFrontColor;
    [SerializeField] Color _holdBackgroundColor;
}
