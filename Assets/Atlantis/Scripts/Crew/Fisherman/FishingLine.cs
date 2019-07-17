using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class FishingLine : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] float _duration;
    [SerializeField] string _distEndSpeedCurveName;
    [SerializeField] string _zEndSpeedCurveName;

    [SerializeField] string _amplitudeCurveName;
    [SerializeField] string _amplitudeSpeedCurveName;

    [Header("Line Settings")]
    [SerializeField] int _pointCount;

    [Header("Linking")]
    [SerializeField] FishingFloat _fishingFloat;

    LineRenderer _renderer;
    Vector3[] _points;

    EvaluationCurve _distEndSpeedCurve;
    EvaluationCurve _zEndSpeedCurve;
    EvaluationCurve _amplitudeCurve;
    EvaluationCurve _amplitudeSpeedCurve;

    Vector3 _p0;
    Vector3 _p1;
    Vector3 _p2;

    Vector3 _from;
    Vector3 _to;

    float _time;
    bool _isLanding;

    void Awake()
    {
        _renderer = GetComponent<LineRenderer>();

        _points = new Vector3[_pointCount];

        _renderer.positionCount = _pointCount;
        _renderer.SetPositions(_points);
    }

    public void Land(Vector3 from, Vector3 to)
    {
        _isLanding = true;

        _from = from;
        _to = to;
        _time = 0f;

        if(_distEndSpeedCurve == null)
        {
            _distEndSpeedCurve = EvaluationCurveManager.instance.GetCurve(_distEndSpeedCurveName);
            _zEndSpeedCurve = EvaluationCurveManager.instance.GetCurve(_zEndSpeedCurveName);
            _amplitudeCurve = EvaluationCurveManager.instance.GetCurve(_amplitudeCurveName);
            _amplitudeSpeedCurve = EvaluationCurveManager.instance.GetCurve(_amplitudeSpeedCurveName);
        }

        _p0 = _from;

        RefreshControlPoints();
        RefreshLinePoints();
        _fishingFloat.Appear();
        _renderer.enabled = true;
    }

    public void StopFishing()
    {
        _isLanding = false;
        _renderer.enabled = false;

        _fishingFloat.Stop();
    }

    void LateUpdate()
    {
        _time += Time.deltaTime;

        if(_isLanding)
        {
            if(_time > _duration)
            {
                _isLanding = false;
                _time = _duration;
            }

            RefreshControlPoints();
            RefreshLinePoints();
        }
    }

    void RefreshControlPoints()
    {
        float distance01 = _distEndSpeedCurve.Evaluate(_time);
        Vector2 pos2D = (_to - _from) * distance01 + _from;

        float z01 = _zEndSpeedCurve.Evaluate(_time);
        float z = (_to.z - _from.z) * z01 + _from.z;

        _p2 = new Vector3(pos2D.x, pos2D.y, z);

        distance01 = _amplitudeSpeedCurve.Evaluate(_time);
        pos2D = (_p2 - _from) * distance01 + _from;

        z01 = _amplitudeCurve.Evaluate(_time);
        z = (_p2.z - _from.z) * z01 + _from.z;

        _p1 = new Vector3(pos2D.x, pos2D.y, z);
    }

    void RefreshLinePoints()
    {
        for (int i = 0; i < _pointCount; i++)
        {
            _points[i] = GetPoint(i / (float)_pointCount);
        }

        _renderer.SetPositions(_points);
    }

    Vector3 GetPoint(float t)
    {
        return Bezier.GetPoint(_p0, _p1, _p2, t);
    }
}
