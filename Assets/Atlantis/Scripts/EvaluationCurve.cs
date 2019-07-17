using UnityEngine;
using System;

[Serializable]
public class EvaluationCurve
{
    public float duration
    {
        get { return _duration; }
    }

    [SerializeField] float _startValue;
    [SerializeField] float _endValue;
    [SerializeField] float _duration;
    [SerializeField] AnimationCurve _curve;

    public float Evaluate(float t)
    {
        return _curve.Evaluate(t / _duration) * (_endValue - _startValue) + _startValue;
    }
}
