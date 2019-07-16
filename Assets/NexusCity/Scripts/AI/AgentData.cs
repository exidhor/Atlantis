using UnityEngine;
using System.Collections;

namespace NexusCity
{
    public class AgentData : ScriptableObject
    {
        [Header("Targeter")]
        [SerializeField] TargetType _targetType;

        public TargetType targetType
        {
            get { return _targetType; }
        }

        [Header("KinematicBody")]
        [SerializeField] float _maxSpeed;
        [SerializeField] float _maxAngular;
        [SerializeField] Vector2 _forward = Vector2.left;
        [SerializeField] float _arriveRadius = 0.01f;

        public float maxSpeed
        {
            get { return _maxSpeed; }
        }

        public float maxAngular
        {
            get { return _maxAngular; }
        }

        public Vector2 forward
        {
            get { return _forward; }
        }

        public float arriveRadius
        {
            get { return _arriveRadius; }
        }
    }
}