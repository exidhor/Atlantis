﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Tools;

public class PlayerShip : MonoSingleton<PlayerShip>
{
    public Vector3 velocity
    {
        get { return _direction; }
    }

    public bool disableInputs
    {
        get { return _disableInputsTime > 0f; }
    }

    public float engineSpeed
    {
        get { return _speed; }
    }

    [Header("Speed Movement")]
    [SerializeField] float _acceleration;
    [SerializeField] AnimationCurve _accelerationCurve;
    [SerializeField] float _decceleration;
    [SerializeField] float _breakSpeed;
    [SerializeField] float _maxSpeed;

    [Header("Rotation Y")]
    [SerializeField] float _minAngularSpeed;
    [SerializeField] float _angularSpeed;
    [SerializeField] float _maxAngularSpeed;
    [SerializeField] float _speedAffectAngular = 0.8f;
    [SerializeField] bool _disableAngularAcceleration;
    [SerializeField] float _angularVariation;

    [Header("Effects")]
    [SerializeField] float _slideStrength;
    [SerializeField] float _swingStrength;

    [Header("Linking")]
    [SerializeField] Text _speedText;
    [SerializeField] Transform _shipModel;
    [SerializeField] PlayerShipBody _body;
    [SerializeField] Rigidbody2D _rb;

    [Header("Infos")]
    [SerializeField, UnityReadOnly] float _speed;
    [SerializeField, UnityReadOnly] float _currentAcceleration;
    [SerializeField, UnityReadOnly] float _angular;

    float _previousAngular;

    Vector3 _direction;
    Vector3 _targetMove;

    float _disableInputsTime;

    float GetAcceleration(float speed)
    {
        float time = speed / _maxSpeed;

        if (time > 1)
            time = 1;

        return _accelerationCurve.Evaluate(time) * _acceleration;
    }

    public void DisableInputs(float time)
    {
        _disableInputsTime = time;
    }

    public void ApplyRepulseForce(Vector2 velocity, float angular, GameObject origin)
    {
        _body.ApplyRepulseForce(velocity, angular, origin);
    }

    public void Move(bool isBreaking, Vector2 inputMove, float dt)
    {
        //float strength = inputMove.magnitude;

        //float angle = Vector2.SignedAngle(_rb.velocity, inputMove);

        //_rb.AddForce(_rb.transform.up * strength);
        //_rb.AddTorque(angle);

        // ---------------------------------------------------------

        _body.Refresh(dt);

        if (disableInputs)
        {
            _disableInputsTime -= dt;
        }

        if (disableInputs)
        {
            _angular = 0f;
            _speed = 0f;
        }
        else 
        {
            if (isBreaking)
            {
                HandleBreak(dt);
                _angular = 0f;
            }
            else
            {
                _targetMove = new Vector3(inputMove.x, inputMove.y, 0f);

                HandleSpeed(dt);
                HandleRotation(dt);
            }
        }

        UpdateRotation(dt);
        UpdateMovement(dt);
    }

    void UpdateRotation(float dt)
    {
        _angular += _body.angular;

        if(!_disableAngularAcceleration)
        {
            _angular = Mathf.Lerp(_previousAngular, _angular, _angularVariation * dt);
        }

        float orientationY = 0f;
        float orientationZ = transform.eulerAngles.z;

        //float max = _angularSpeed * _speed * _speedAffectAngular;

        if (Mathf.Abs(_angular) > _maxAngularSpeed)
        {
            _angular = Mathf.Sign(_angular) * _maxAngularSpeed;
        }

        float angleY = orientationY + _swingStrength * _angular * dt; 
        float angleZ = orientationZ + _angular * dt;
        //transform.localRotation = Quaternion.Euler(0, 0, angleZ);
        _shipModel.localRotation = Quaternion.Euler(0, angleY, 0f);

        _rb.angularVelocity = _angular;

        _previousAngular = _angular;
    }

    void UpdateMovement(float dt)
    {
        _direction = transform.up * _speed;
        _direction.x += _body.velocity.x;
        _direction.y += _body.velocity.y;

        if(_direction.magnitude > _maxSpeed)
        {
            _direction.Normalize();
            _direction *= _maxSpeed;
        }

        Vector3 move = _direction * dt;

        Vector2 move2d = new Vector2(move.x, move.y);
        float angle = -_angular * dt * _slideStrength * (_speed / _maxSpeed);
        move2d = MathHelper.RotateVector(move2d, angle * Mathf.Deg2Rad);
        move.x = move2d.x;
        move.y = move2d.y;

        //transform.localPosition += move;

        _rb.velocity = move2d / dt;
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
            //float maxSpeed = _speed + _acceleration * dt;

            _currentAcceleration = GetAcceleration(_speed);
            float maxSpeed = _speed + _currentAcceleration * dt;

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
        Vector3 forward = transform.up;

        float angle = Vector3.SignedAngle(forward, _targetMove, Vector3.forward);

        float angular = _angularSpeed * (1 + _speedAffectAngular * _speed);
        angular = Mathf.Lerp(_minAngularSpeed * Mathf.Sign(angular), angular, Mathf.Abs(angle) / 180f);

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

    void LateUpdate()
    {
        _speedText.text = (int)_speed + " MpS";
    }
}
