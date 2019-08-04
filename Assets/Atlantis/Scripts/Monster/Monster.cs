using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Tools;

public class Monster : QTCircleCollider
{
    [SerializeField] NavMeshAgent _agent;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        _agent.destination = PlayerShip.instance.transform.position;
    }
}
