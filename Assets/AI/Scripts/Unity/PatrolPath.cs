using UnityEngine;
using System.Collections.Generic;
using System;

namespace UnityAI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/PatrolPath")]
    public class PatrolPath : ScriptableObject
    {
        public List<Vector3> points
        {
            get { return _points; }
        }

        public bool cycle
        {
            get { return _cycle; }
        }

        [SerializeField] bool _cycle;
        [SerializeField] bool _spawnOnPoint;
        [SerializeField] List<Vector3> _points = new List<Vector3>();

        public void Next(Steering steering)
        {
            int next = steering.patrolIndex + steering.patrolDirection;

            if(next < 0)
            {
                if(cycle)
                {
                    next = points.Count - 1;
                }
                else
                {
                    next = 1;
                    steering.patrolDirection = 1;
                }
            }
            else if(next > points.Count - 1)
            {
                if (cycle)
                {
                    next = 0;
                }
                else
                {
                    next = points.Count - 2;
                    steering.patrolDirection = -1;
                }
            }

            steering.patrolIndex = next;
        }
    }
}
