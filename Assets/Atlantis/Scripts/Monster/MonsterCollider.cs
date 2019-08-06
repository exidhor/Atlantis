using UnityEngine;
using System.Collections;
using Tools;

public class MonsterCollider : QTCircleCollider
{
    public readonly float ENABLE_DEPTH_LIMIT = -1f;

    public Monster monster
    {
        get { return _monster; }
    }

    [Header("Monster")]
    [SerializeField] Monster _monster;

    public void SetIsEnable(float depth)
    {
        _enable = depth > ENABLE_DEPTH_LIMIT;
    }
}
