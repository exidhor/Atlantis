using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class MonsterInfo
{
    public Monster model
    {
        get { return _model; }
    }

    [SerializeField] Monster _model;
}
