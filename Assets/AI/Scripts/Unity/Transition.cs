using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAI
{
    [System.Serializable]
    public class Transition
    {
        [SerializeField] Decision _decision;
        [SerializeField] State _trueState;
        [SerializeField] State _falseState;

        public State FindNextState(Steering steering)
        {
            if(_decision.Decide(steering))
            {
                return _trueState;
            }

            return _falseState;
        }
    }
}