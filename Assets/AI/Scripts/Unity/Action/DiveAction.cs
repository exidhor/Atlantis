using UnityEngine;
using System.Collections;

namespace UnityAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Dive")]
    public class DiveAction : Action
    {
        [SerializeField] float _diveDepth;
        [SerializeField] float _diveSpeed;

        public override void Init()
        {
            // nothing yet
        }

        public override void Act(Steering steering)
        {
            Dive(steering);
        }

        void Dive(Steering steering)
        {
            float currentDepth = steering.mondelTransform.position.y;

            steering.SetColliderEnable(currentDepth);

            if(currentDepth < _diveDepth)
            {
                return;
            }

            float diveMove = _diveSpeed * Time.deltaTime;

            currentDepth += diveMove;

            if(currentDepth < _diveDepth)
            {
                currentDepth = _diveDepth;
            }

            Vector3 pos = steering.mondelTransform.position;
            pos.y = currentDepth;
            steering.mondelTransform.position = pos;
        }
    }
}

