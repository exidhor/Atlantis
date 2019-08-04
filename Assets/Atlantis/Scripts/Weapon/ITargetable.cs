using UnityEngine;
using System.Collections;

public interface ITargetable
{
    Vector3 position { get; }

    void DealDamage(int damage);
}
