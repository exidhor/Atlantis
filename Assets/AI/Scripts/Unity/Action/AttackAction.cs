using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
    public class AttackAction : Action
    {
        public override void Init()
        {
            // nothing yet
        }

        public override void Act(Steering steering)
        {
            Attack(steering);
        }

        private void Attack(Steering steering)
        {
            RaycastHit hit;

            Debug.DrawRay(steering.eyes.position, 
                          steering.eyes.forward.normalized * steering.properties.attackRange, 
                          Color.red);

            if (Physics.SphereCast(steering.eyes.position, 
                                   steering.properties.lookSphereCastRadius, 
                                   steering.eyes.forward, 
                                   out hit, 
                                   steering.properties.attackRange)
                && hit.collider.CompareTag("Player"))
            {
                if (steering.stateTimeElapsed >= steering.properties.attackRate)
                {
                    // todo : attack
                    //controller.tankShooting.Fire(controller.enemyStats.attackForce, controller.enemyStats.attackRate);
                }
            }
        }
    }
}
