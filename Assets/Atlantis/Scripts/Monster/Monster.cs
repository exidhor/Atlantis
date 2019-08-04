using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Tools;

public class Monster : QTCircleCollider, ITargetable
{
    Vector3 ITargetable.position
    {
        get { return new Vector3(center.x, transform.position.y, center.y); }
    }

    [Header("Monster Specs")]
    [SerializeField] NavMeshAgent _agent;

    public void DealDamage(int damage)
    {
        // todo
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        _agent.destination = PlayerShip.instance.transform.position;
    }
}
