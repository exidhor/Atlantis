using UnityEngine;
using System.Collections.Generic;
using System;
using Tools;
using UnityEngine.AI;

namespace UnityAI
{
    [Serializable]
    public class Steering
    {
        public Transform eyes
        {
            get { return _eyes; }
        }

        public State currentState
        {
            get { return _currentState; }
        }

        public CharacterProperties properties
        {
            get { return _properties; }
        }

        public NavMeshAgent agent
        {
            get { return _agent; }
        }

        public Transform controllerTransform
        {
            get { return _controllerTransform; }
        }

        public Transform mondelTransform
        {
            get { return _modelTransform; }
        }

        [SerializeField] State _currentState;
        [SerializeField] Transform _eyes;
        [SerializeField] CharacterProperties _properties;

        [NonSerialized] public Transform target;
        [NonSerialized] public int patrolDirection = 1;
        [NonSerialized] public int patrolIndex;
        [UnityReadOnly] public float stateTimeElapsed;

        NavMeshAgent _agent;
        Transform _controllerTransform;
        Transform _modelTransform;

        public void Init(NavMeshAgent agent, 
                         Transform controllerTransform,
                         Transform modelTransform)
        {
            _agent = agent;
            _controllerTransform = controllerTransform;
            _modelTransform = modelTransform;
        }

        public void SetState(State state)
        {
            if(state != null && state != currentState)
            {
                _currentState = state;
                stateTimeElapsed = 0f;
            }
        }

        public void Actualize()
        {
            stateTimeElapsed += Time.deltaTime;
            currentState.UpdateState(this);
        }
    }
}
