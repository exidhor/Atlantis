using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using Complete;

namespace UnityAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class StateController : MonoBehaviour
    {
        [HideInInspector] public NavMeshAgent navMeshAgent;

        [SerializeField] Steering _steering = new Steering();
        [SerializeField] Transform _model;
        [SerializeField] MonsterCollider _collider;

        private bool aiActive;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();

            navMeshAgent.enabled = true;
            _steering.Init(navMeshAgent, transform, _model, _collider);
        }

        void Update()
        {
            _steering.Actualize();
        }

        void OnDrawGizmos()
        {
            if (_steering.currentState != null && _steering.eyes != null)
            {
                Gizmos.color = _steering.currentState.sceneGizmoColor;
                Gizmos.DrawWireSphere(_steering.eyes.position,
                                      _steering.properties.lookSphereCastRadius);
            }
        }
    }
}