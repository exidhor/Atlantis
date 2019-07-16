using UnityEngine;
using System.Collections;

namespace NexusCity
{
    public class KinematicBody : MonoBehaviour
    {
        [Header("Infos")]
        [SerializeField] float _maxSpeed;
        [SerializeField] float _maxAngular;
        [SerializeField] Vector2 _forward = Vector2.left;
        [SerializeField] float _arriveRadius = 0.01f;

        public void Init(AgentData data)
        {
            _maxSpeed = data.maxSpeed;
            _maxAngular = data.maxAngular;
            _forward = data.forward;
            _arriveRadius = data.arriveRadius;
        }

        public void Actualize(Vector2 target, float dt)
        {
            Vector2 forward = transform.TransformVector(_forward);

            Vector2 pos = transform.position;
            Vector2 move = target - pos;
            float speed = _maxSpeed * dt;

            float sqrLeft = move.sqrMagnitude;

            if(sqrLeft < _arriveRadius)
            {
                speed = 0;
            }
            else if (sqrLeft < speed * speed)
            {
                speed = move.magnitude;
            }

            float orientation = transform.eulerAngles.z;
            float angle = Vector2.SignedAngle(forward, move);

            float angular = _maxAngular * dt;
                
            if(Mathf.Abs(angle) < angular)
            {
                angular = angle;
            }
            else
            {
                angular = angular * Mathf.Sign(angle);
            }

            transform.localRotation = Quaternion.Euler(0, 0, orientation + angular);
            transform.position += transform.TransformVector(_forward) * speed;
        }
    }
}