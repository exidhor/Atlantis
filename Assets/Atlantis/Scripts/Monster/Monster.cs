using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using MemoryManagement;
using Tools;

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

    [Header("Monster Specs")]
    [SerializeField] Transform _target;
    [SerializeField] MonsterCollider _collider;
    [SerializeField] NavMeshAgent _agent;

    MonsterZone _zone;

    public void SetZone(MonsterZone zone)
    {
        _zone = zone;
    }

    public void DealDamage(int damage)
    {
        // todo
    }

    protected void Update()
    {
        //_agent.destination = PlayerShip.instance.transform.position;

        _agent.destination = _target.transform.position;
    }
}
