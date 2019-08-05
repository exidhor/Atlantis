using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAI
{
    [CreateAssetMenu(menuName = "PluggableAI/State")]
    public class State : ScriptableObject
    {
        public Action[] actions;
        public Transition[] transitions;
        public Color sceneGizmoColor = Color.grey;

        [Header("Max Time")]
        [SerializeField] State _maxTimeState;
        [SerializeField] float _maxTime = 0f;

        public void UpdateState(Steering steering)
        {
            DoActions(steering);
            CheckTransitions(steering);
        }

        private void DoActions(Steering steering)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Act(steering);
            }
        }

        private void CheckTransitions(Steering steering)
        {
            if (HasReachMaxTime(steering.stateTimeElapsed))
            {
                steering.SetState(_maxTimeState);
                return;
            }

            for (int i = 0; i < transitions.Length; i++)
            {
                steering.SetState(transitions[i].FindNextState(steering));

                //bool decisionSucceeded = transitions[i].decision.Decide(steering);

                //if (decisionSucceeded)
                //{
                //    steering.SetState(transitions[i]._trueState);
                //}
                //else
                //{
                //    steering.SetState(transitions[i]._falseState);
                //}
            }
        }

        bool HasReachMaxTime(float currentTime)
        {
            return _maxTimeState != null
                   && _maxTime > 0f
                   && currentTime > _maxTime;
        }
    }
}