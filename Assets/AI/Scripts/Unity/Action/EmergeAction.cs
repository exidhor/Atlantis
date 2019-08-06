using UnityEngine;
using System.Collections;

namespace UnityAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Emerge")]
    public class EmergeAction : Action
    {
        [SerializeField] float _emergeSpeed;

        public override void Init()
        {
            // nothing yet
        }

        public override void Act(Steering steering)
        {
            Emerge(steering);
        }

        void Emerge(Steering steering)
        {
            float currentDepth = steering.mondelTransform.position.y;

            steering.SetColliderEnable(currentDepth);

            if (currentDepth >= 0f)
            {
                return;
            }

            float emergeMove = _emergeSpeed * Time.deltaTime;

            currentDepth += emergeMove;

            if (currentDepth > 0f)
            {
                currentDepth = 0f;
            }

            Vector3 pos = steering.mondelTransform.position;
            pos.y = currentDepth;
            steering.mondelTransform.position = pos;
        }
    }
}

