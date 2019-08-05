using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Scan")]
    public class ScanDecision : Decision
    {
        public override bool Decide(Steering steering)
        {
            bool noEnemyInSight = Scan(steering);
            return noEnemyInSight;
        }

        private bool Scan(Steering steering)
        {
            steering.agent.isStopped = true;
            steering.controllerTransform.Rotate(0, 
                                                steering.properties.searchingTurnSpeed * Time.deltaTime, 
                                                0);

            return steering.stateTimeElapsed >= steering.properties.searchDuration;
        }
    }
}
