using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Tools;

public class Monster : QTCircleCollider, ITargetable
{
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
