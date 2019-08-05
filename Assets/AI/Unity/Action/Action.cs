using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAI
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(StateController controller);
    }
}