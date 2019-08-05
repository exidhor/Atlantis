using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/ActiveState")]
    public class ActiveStateDecision : Decision
    {
        public override bool Decide(Steering steering)
        {
            bool chaseTargetIsActive = steering.target.gameObject.activeSelf;
            return chaseTargetIsActive;
        }
    }
}