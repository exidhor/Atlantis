using UnityEngine;
using System.Collections;
using Tools;

public class MonsterCollider : QTCircleCollider
{
    public Monster monster
    {
        get { return _monster; }
    }

    [Header("Monster")]
    [SerializeField] Monster _monster;
}
