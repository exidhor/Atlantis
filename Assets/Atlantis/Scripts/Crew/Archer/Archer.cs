using UnityEngine;
using System.Collections.Generic;
using Tools;

public class Archer : Crew
{
    public override CrewType type
    {
        get { return CrewType.Archer; }
    }

    public override float progress01
    {
        get { return _currentTime / _targetTime; }
    }

    [Header("Logic")]
    [SerializeField] float _targetTime;

    QTCircleCollider _collider;
    float _currentTime;

    // todo
}
