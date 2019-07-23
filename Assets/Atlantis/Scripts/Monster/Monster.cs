using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Monster : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;

    // Update is called once per frame
    void Update()
    {
        _agent.destination = PlayerShip.instance.transform.position;
    }
}
