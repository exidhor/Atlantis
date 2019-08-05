using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
    public class LookDecision : Decision
    {
        public override bool Decide(Steering steering)
        {
            bool targetVisible = Look(steering);
            return targetVisible;
        }

        private bool Look(Steering steering)
        {
            //RaycastHit hit;

            Debug.DrawRay(steering.eyes.position, 
                          steering.eyes.forward.normalized * steering.properties.lookRange, 
                          Color.green);

            Vector3 source = steering.eyes.position;
            Vector3 target = PlayerShip.instance.transform.position;
            NavMeshHit hit;

            if (NavMesh.Raycast(steering.eyes.position, target, out hit, NavMesh.AllAreas))
            {
                // todo
                return true;
            }

            //if (Physics.SphereCast(steering.eyes.position, 
            //                   steering.properties.lookSphereCastRadius, 
            //                   steering.eyes.forward, 
            //                   out hit, 
            //                   steering.properties.lookRange)
            //&& hit.collider.CompareTag("Player"))
            //{
            //    steering.target = hit.transform;
            //    return true;
            //}
            else
            {
                return false;
            }
        }
    }
}