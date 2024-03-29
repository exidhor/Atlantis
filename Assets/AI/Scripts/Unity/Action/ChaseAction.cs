﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void Init()
        {
            // nothing yet
        }

        public override void OnStart()
        {
            // nothing yet
        }

        public override void Act(Steering steering)
        {
            Chase(steering);
        }

        private void Chase(Steering steering)
        {
            steering.agent.destination = steering.target.position;
            steering.agent.isStopped = false;
        }
    }
}