using UnityEngine;
using System.Collections;

public interface ITargetable
{
    Transform transform { get; }

    void DealDamage(int damage);
}
