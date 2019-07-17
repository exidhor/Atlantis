using UnityEngine;
using System.Collections;

public class Force
{
    public Vector2 currentVelocity
    {
        get { return _currentVelocity; }
    }

    public float currentAngular
    {
        get { return _currentAngular; }
    }

    public GameObject origin
    {
        get { return _origin; }
    }

    public bool isFinish
    {
        get 
        {
            return _time > _velocityCurve.duration
                   && _time > _angularCurve.duration;
        }
    }

    private Vector2 _currentVelocity;
    private float _currentAngular;

    private Vector2 _velocity;
    private float _angular;

    private EvaluationCurve _velocityCurve;
    private EvaluationCurve _angularCurve;

    private float _time;

    private GameObject _origin;

    public void Init(Vector2 velocity, 
                     float angular, 
                     EvaluationCurve velocityCurve,
                     EvaluationCurve angularCurve,
                     GameObject origin = null)
    {
        _velocity = velocity;
        _angular = angular;
        _velocityCurve = velocityCurve;
        _angularCurve = angularCurve;

        _origin = origin;

        _time = 0;
    }

    public void Refresh(float dt)
    {
        _time += dt;

        float speed = _velocityCurve.Evaluate(_time);
        _currentVelocity = _velocity * speed;

        _currentAngular = _angularCurve.Evaluate(_time) * _angular;
    }
}
