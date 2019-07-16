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
    [SerializeField] float _slideStrength;

    [SerializeField] float _angularSpeed;
    [SerializeField] float _speedAffectAngular = 0.8f;

    [SerializeField, UnityReadOnly] float _speed;

    Vector3 _targetMove;
    float _currentSpeed;
    float _angle;

    public void Move(bool isBreaking, Vector2 inputMove, float dt)
    {
        if(isBreaking)
        {
            Debug.Log("Break");
            HandleBreak(dt);
            _angle = 0;
        }
        else
        {
            _targetMove = new Vector3(inputMove.x, 0, inputMove.y);

            HandleSpeed(dt);
            HandleRotation(dt);
        }

        Vector3 move = transform.forward * _currentSpeed;


        Vector2 move2d = new Vector2(move.x, move.z);
        float orientation = transform.rotation.eulerAngles.y;
        move2d = MathHelper.RotateVector(move2d, (-_angle * _slideStrength * (_speed / _maxSpeed)) * Mathf.Deg2Rad);
        move.x = move2d.x;
        move.z = move2d.y;

        transform.position += move;
    }

    void HandleBreak(float dt)
    {
        _speed += _breakSpeed;

        if (_speed < 0)
            _speed = 0;

        _currentSpeed = _speed * dt;
    }

    void HandleSpeed(float dt)
    {
        Vector3 move = _targetMove;

        float speed = move.magnitude;
        move.Normalize();

        if (speed > _speed)
        {
            float maxSpeed = _speed + _acceleration * dt;

            if (maxSpeed < speed)
            {
                speed = maxSpeed;
            }
        }
        else
        {
            float minSpeed = _speed * _decceleration * dt;

            if (minSpeed > speed)
            {
                speed = minSpeed;
            }
        }

        if(speed > _maxSpeed)
        {
            speed = _maxSpeed;
        }

        _speed = speed;
        _currentSpeed = speed * dt;
    }

    void HandleRotation(float dt)
    {
        Vector3 forward = transform.forward;

        float orientation = transform.eulerAngles.y;
        float angle = Vector3.SignedAngle(forward, _targetMove, Vector3.up);

        float angular = _angularSpeed * dt * _speedAffectAngular * _speed;

        if (Mathf.Abs(angle) < angular)
        {
            angular = angle;
        }
        else
        {
            angular = angular * Mathf.Sign(angle);
        }

        _angle = angular;
        transform.localRotation = Quaternion.Euler(0, orientation + angular, 0);
    }
}
