using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Tools;

public class PlayerShip : MonoSingleton<PlayerShip>
{
    public Vector3 velocity
    {
        get { return _direction; }
    }

    public float speed
    {
        get { return _speed; }
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

    [Header("Linking")]
    [SerializeField] Text _speedText;
    [SerializeField] Transform _shipModel;
    [SerializeField] PlayerShipBody _body;

    [Header("Infos")]
    [SerializeField, UnityReadOnly] float _speed;
    [SerializeField, UnityReadOnly] float _angular;

    float _previousAngular;

    Vector3 _direction;
    Vector3 _targetMove;

    float _disableInputsTime;

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
        _body.Refresh(dt);

        if (_disableInputsTime > 0)
        {
            _disableInputsTime -= dt;
        }

        if (_disableInputsTime > 0)
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
        _angular = Mathf.Lerp(_previousAngular, _angular, _dampling * dt);

        float orientationY = 0f;
        float orientationZ = transform.eulerAngles.z;

        _angular += _body.angular;

        float angleY = orientationY + _swingStrength * _angular * dt; 
        float angleZ = orientationZ + _angular * dt;
        transform.localRotation = Quaternion.Euler(0, 0, angleZ);
        _shipModel.localRotation = Quaternion.Euler(0, angleY, 0f);

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

        transform.localPosition += move;
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
        Vector3 forward = transform.up;

        float angle = Vector3.SignedAngle(forward, _targetMove, Vector3.forward);

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

    void LateUpdate()
    {
        _speedText.text = "Speed : " + _speed;
    }
}
