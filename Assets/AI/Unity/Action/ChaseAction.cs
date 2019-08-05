using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void Act(StateController controller)
        {
            Chase(controller);
        }

        private void Chase(StateController controller)
        {
            controller.navMeshAgent.destination = controller.chaseTarget.position;
            controller.navMeshAgent.Resume();
        }
    }
}