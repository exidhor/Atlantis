using UnityEngine;
using System.Collections;
using Tools;

public class PlayerShip : MonoBehaviour
{
    public Vector3 direction
    {
        get { return _direction; }
    }

    [Header("Speed Movement")]
    [SerializeField] float _acceleration;
    [SerializeField] float _decceleration;
    [SerializeField] float _breakSpeed;
    [SerializeField] float _maxSpeed;

    [Header("Rotation Y")]
    [SerializeField] float _angularSpeed;
    [SerializeField] float _speedAffectAngular = 0.8f;
    [SerializeField] float _dampling;

    [Header("Effects")]
    [SerializeField] float _slideStrength;
    [SerializeField] float _swingStrength;

    [Header("Infos")]
    [SerializeField, UnityReadOnly] float _speed;
    [SerializeField, UnityReadOnly] float _angular;

    float _previousAngular;

    Vector3 _direction;
    Vector3 _targetMove;

    public void Move(bool isBreaking, Vector2 inputMove, float dt)
    {
        if (isBreaking)
        {
            HandleBreak(dt);
            _angular = 0;
        }
        else
        {
            _targetMove = new Vector3(inputMove.x, 0, inputMove.y);

            HandleSpeed(dt);
            HandleRotation(dt);
        }

        UpdateRotation(dt);
        UpdateMovement(dt);
    }

    void UpdateRotation(float dt)
    {
        _angular = Mathf.Lerp(_previousAngular, _angular, _dampling * dt);

        float orientationY = transform.eulerAngles.y;
        float orientationZ = 0f; //transform.eulerAngles.z;

        float angleY = orientationY + _angular * dt;
        float angleZ = orientationZ + _swingStrength * _angular * dt;
        transform.localRotation = Quaternion.Euler(0, angleY, angleZ);

        _previousAngular = _angular;
    }

    void UpdateMovement(float dt)
    {
        Vector3 move = transform.forward * _speed * dt;
        _direction = transform.forward * _speed;

        Vector2 move2d = new Vector2(move.x, move.z);
        float angle = -_angular * dt * _slideStrength * (_speed / _maxSpeed);
        move2d = MathHelper.RotateVector(move2d, angle * Mathf.Deg2Rad);
        move.x = move2d.x;
        move.z = move2d.y;

        transform.position += move;
    }

    void HandleBreak(float dt)
    {
        _speed += _breakSpeed;

        if (_speed < 0)
            _speed = 0;
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
    }

    void HandleRotation(float dt)
    {
        Vector3 forward = transform.forward;

        float angle = Vector3.SignedAngle(forward, _targetMove, Vector3.up);

        float angular = _angularSpeed * _speedAffectAngular * _speed;
        float targetAngular = angular * dt;

        if (Mathf.Abs(angle) < targetAngular)
        {
            angular = angle;
        }
        else
        {
            angular *= Mathf.Sign(angle);
        }

        _angular = angular;
    }
}
