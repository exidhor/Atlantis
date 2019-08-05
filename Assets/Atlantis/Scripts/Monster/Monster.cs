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

    protected new MonsterCollider collider
    {
        get { return _collider; }
    }

    [Header("Monster Specs")]
    [SerializeField] int _life = 10;
    [SerializeField] MonsterCollider _collider;

    MonsterZone _zone;

    Vector3 _targetPos;

    protected abstract void OnUpdate(float dt);

    public void SetZone(MonsterZone zone)
    {
        _zone = zone;
    }

    public void DealDamage(int damage)
    {
        _life -= damage;

        // todo
    }

    void Update()
    {
        OnUpdate(Time.deltaTime);

        //_agent.destination = PlayerShip.instance.transform.position;

        //if (_targetPos != _target.transform.position)
        //{
        //    _targetPos = _target.transform.position;
        //    bool result = _agent.SetDestination(_targetPos);

        //    Debug.Log("Result angent = " + result + " Path Status " + _agent.pathStatus);
        //}
    }
}
