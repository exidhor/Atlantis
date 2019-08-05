using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BulletInfo
{
    public Bullet model
    {
        get { return _model; }
    }

    [SerializeField] Bullet _model;
}
