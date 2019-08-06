using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using MemoryManagement;
using Tools;
using UnityAI;

public abstract class Monster : UnityPoolObject, ITargetable
{
    public abstract MonsterType type { get; }

    Vector3 ITargetable.position
    {
        get 
        { 
            return new Vector3(_collider.center.x, 
                               transform.position.y, 
                               _collider.center.y);
        }
    }

    protected new MonsterCollider collider
    {
        get { return _collider; }
    }

    [Header("Monster Specs")]
    [SerializeField] int _life = 10;
    [SerializeField] MonsterCollider _collider;
    [SerializeField] StateController _ai;

    MonsterZone _zone;

    public void SetZone(MonsterZone zone)
    {
        _zone = zone;
    }

    public void DealDamage(int damage)
    {
        _life -= damage;

        // todo
    }

    protected internal override void OnPreUsing()
    {
        base.OnPreUsing();

        _zone = null; 
    }

    public override void OnRelease()
    {
        base.OnRelease();

        if(_zone != null)
        {
            _zone.Unregister(this);
        }
    }
}
