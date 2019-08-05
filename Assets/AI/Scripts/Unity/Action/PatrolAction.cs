using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
    public class PatrolAction : Action
    {
        [SerializeField] float _arriveDistance = 3f;
        [SerializeField] PatrolPath _path;

        public override void Act(Steering steering)
        {
            Patrol(steering);
        }

        private void Patrol(Steering steering)
        {
            steering.agent.destination = _path.points[steering.patrolIndex];
            steering.agent.isStopped = false;

            if (steering.agent.remainingDistance <= _arriveDistance
                && !steering.agent.pathPending)
            {
                //steering.patrolIndex = (steering.patrolIndex + 1) % steering.wayPointList.Count;
                _path.Next(steering);
            }
        }
    }
}
