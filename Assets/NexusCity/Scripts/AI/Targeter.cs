using UnityEngine;
using System.Collections;

namespace NexusCity
{
    public class Targeter : MonoBehaviour
    {
        public ITarget currentTarget
        {
            get { return _currentTarget; }
        }

        ITarget _currentTarget;

        public void Init(AgentData data)
        {
            switch(data.targetType)
            {
                case TargetType.Player:
                    _currentTarget = FindTarget();
                    break;

                default:
                    Debug.LogError("Unknown target type : " + data.targetType);
                    break;
            }
        }

        public void Actualize(float dt)
        {
            //if(_currentTarget == null)
            //{
            //    _currentTarget = FindTarget();
            //}
        }

        static ITarget FindTarget()
        {
            return new PointTarget(Vector2.zero);
            //return new TransformTarget(Player.instance.transform);
        }
    }
}