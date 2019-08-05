using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public abstract class AgentMonster : Monster
{
    //protected NavMeshAgent agent
    //{
    //    get { return _agent; }
    //}

    [Header("Agent")]
    [SerializeField] NavMeshAgent _agent;

    Vector3 _target;

    protected abstract void OnPathEnd();

    protected void SetSpeed(float speed)
    {
        _agent.speed = speed;
    }

    protected void SetTargetAgent(Vector3 target)
    {
        if (_target != target)
        {
            _agent.SetDestination(target);
            _target = target;
        }
    }

    protected override void OnUpdate(float dt)
    {
        if(_agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            OnPathEnd();
        }
    }
}
