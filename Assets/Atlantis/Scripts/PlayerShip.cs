using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour
{
    [Header("Speed controls")]
    [SerializeField] float _acceleration;
    [SerializeField] float _decceleration;
    [SerializeField] float _breakSpeed;
    //[SerializeField] float _startSpeed;
    [SerializeField] float _maxSpeed;

    [SerializeField] float _angularSpeed;

    Vector3 _targetMove;
    Vector3 _move;

    float _currentSpeed = 0f;

    public void Move(bool isBreaking, Vector2 inputMove, float dt)
    {
        if(isBreaking)
        {
            HandleBreak(dt);
        }
        else
        {
            _targetMove = new Vector3(inputMove.x, 0, inputMove.y);

            HandleSpeed(dt);
            HandleRotation(dt);
            transform.position += _move;
        }
    }

    void HandleBreak(float dt)
    {
        _currentSpeed += _breakSpeed * dt;

        if (_currentSpeed < 0)
            _currentSpeed = 0;
    }

    void HandleSpeed(float dt)
    {
        _move = _targetMove * dt;

        float speed = _move.magnitude;
        _move.Normalize();

        if (speed > _currentSpeed)
        {
            float maxSpeed = _currentSpeed + _acceleration * dt;

            if (maxSpeed < speed)
            {
                speed = maxSpeed;
            }
        }
        else
        {
            float minSpeed = _currentSpeed * _decceleration * dt;

            if (minSpeed > speed)
            {
                speed = minSpeed;
            }
        }

        _move *= speed;
    }

    void HandleRotation(float dt)
    {
        Vector2 forward = transform.forward;

        Vector2 pos = transform.position;

        float orientation = transform.eulerAngles.z;
        float angle = Vector2.SignedAngle(forward, _targetMove);

        float angular = _angularSpeed * dt;

        if (Mathf.Abs(angle) < angular)
        {
            angular = angle;
        }
        else
        {
            angular = angular * Mathf.Sign(angle);
        }

        transform.localRotation = Quaternion.Euler(0, 0, orientation + angular);
    }
}
