using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using Complete;

namespace UnityAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class StateController : MonoBehaviour
    {
        //public State currentState;
        //public EnemyStats enemyStats;
        //public Transform eyes;
        //public State remainState;

        [HideInInspector] public NavMeshAgent navMeshAgent;
        //[HideInInspector] public Complete.TankShooting tankShooting;
        //[HideInInspector] public List<Transform> wayPointList;
        //[HideInInspector] public int nextWayPoint;
        //[HideInInspector] public Transform chaseTarget;
        //[HideInInspector] public float stateTimeElapsed;

        [SerializeField] Steering _steering = new Steering();
        [SerializeField] Transform _model;

        private bool aiActive;

        void Awake()
        {
            //tankShooting = GetComponent<Complete.TankShooting>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            navMeshAgent.enabled = true;
            _steering.Init(navMeshAgent, transform, _model);
        }

        //public void SetupAI(bool aiActivationFromTankManager, 
        //                    List<Transform> wayPointsFromTankManager)
        //{
        //    wayPointList = wayPointsFromTankManager;
        //    aiActive = aiActivationFromTankManager;
        //    if (aiActive)
        //    {
        //        navMeshAgent.enabled = true;
        //    }
        //    else
        //    {
        //        navMeshAgent.enabled = false;
        //    }
        //}

        void Update()
        {
            //if (!aiActive)
                //return;

            _steering.Actualize();
        }

        //public void TransitionToState(State nextState)
        //{
        //    //if (nextState != remainState)
        //    if(nextState != currentState)
        //    {
        //        currentState = nextState;
        //        OnExitState();
        //    }
        //}

        //public bool CheckIfCountDownElapsed(float duration)
        //{
        //    stateTimeElapsed += Time.deltaTime;
        //    return (stateTimeElapsed >= duration);
        //}

        //private void OnExitState()
        //{
        //    stateTimeElapsed = 0;
        //}

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