using UnityEngine;
using System.Collections;
using Tools;

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
    [SerializeField, UnityReadOnly] float _currentSpeed;

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
        }

        transform.position += transform.forward * _currentSpeed;
    }

    void HandleBreak(float dt)
    {
        _currentSpeed += _breakSpeed * dt;

        if (_currentSpeed < 0)
            _currentSpeed = 0;
    }

    void HandleSpeed(float dt)
    {
        Vector3 move = _targetMove;

        float speed = move.magnitude;
        move.Normalize();

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

        _currentSpeed = speed;
        _currentSpeed = speed * dt;
    }

    void HandleRotation(float dt)
    {
        Vector3 forward = transform.forward;

        float orientation = transform.eulerAngles.y;
        float angle = Vector3.SignedAngle(forward, _targetMove, Vector3.up);

        float angular = _angularSpeed * dt;

        if (Mathf.Abs(angle) < angular)
        {
            angular = angle;
        }
        else
        {
            angular = angular * Mathf.Sign(angle);
        }

        transform.localRotation = Quaternion.Euler(0, orientation + angular, 0);

        Debug.Log("Angle " + angle);
    }
}
