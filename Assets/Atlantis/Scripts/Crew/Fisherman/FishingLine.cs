using UnityEngine;
using System;

[RequireComponent(typeof(LineRenderer))]
public class FishingLine : MonoBehaviour
{
    [Serializable]
    class Animation
    {
        public float duration;
        [SerializeField] string _distEndSpeedCurveName;
        [SerializeField] string _zEndSpeedCurveName;

        [SerializeField] string _amplitudeCurveName;
        [SerializeField] string _amplitudeSpeedCurveName;

        [NonSerialized] public EvaluationCurve distEndSpeedCurve;
        [NonSerialized] public EvaluationCurve zEndSpeedCurve;
        [NonSerialized] public EvaluationCurve amplitudeCurve;
        [NonSerialized] public EvaluationCurve amplitudeSpeedCurve;

        public void Init()
        {
            if (distEndSpeedCurve == null)
            {
                distEndSpeedCurve = EvaluationCurveManager.instance.GetCurve(_distEndSpeedCurveName);
                zEndSpeedCurve = EvaluationCurveManager.instance.GetCurve(_zEndSpeedCurveName);
                amplitudeCurve = EvaluationCurveManager.instance.GetCurve(_amplitudeCurveName);
                amplitudeSpeedCurve = EvaluationCurveManager.instance.GetCurve(_amplitudeSpeedCurveName);
            }
        }
    }

    [Header("Animation")]
    [SerializeField] Animation _landAnimation;
    [SerializeField] Animation _stopAnimation;

    [Header("Line Settings")]
    [SerializeField] int _pointCount;

    [Header("Linking")]
    [SerializeField] Transform _fisherman;
    [SerializeField] FishingFloat _fishingFloat;

    LineRenderer renderer
    {
        get
        {
            if(_renderer == null)
            {
                _renderer = GetComponent<LineRenderer>();
            }

            return _renderer;
        }
    }

    public bool isStopping
    {
        get { return _isStopping; }
    }

    public bool isLanding
    {
        get { return _isLanding; }
    }

    Animation _currentAnimation;

    LineRenderer _renderer;

    Vector3[] _points;

    EvaluationCurve _distEndSpeedCurve;
    EvaluationCurve _zEndSpeedCurve;
    EvaluationCurve _amplitudeCurve;
    EvaluationCurve _amplitudeSpeedCurve;

    Vector3 _p0;
    Vector3 _p1;
    Vector3 _p2;

    Vector3 _end;
    Vector3 _to;

    Vector3 from
    {
        get { return _fisherman.position; }
    }

    float _time;
    bool _isLanding;
    bool _isLanded;
    bool _isStopping;

    void Awake()
    {
        _points = new Vector3[_pointCount];

        renderer.positionCount = _pointCount;
        renderer.SetPositions(_points);
    }

    public void Land(Vector3 to)
    {
        _isLanding = true;
        _isLanded = true;

        _to = to;
        _time = 0f;

        _landAnimation.Init();

        _currentAnimation = _landAnimation;

        _p0 = from;

        RefreshControlPoints(0f);
        RefreshLinePoints(true, false);
        _fishingFloat.Appear();
        renderer.enabled = true;
    }

    public void StopFishing()
    {
        if(_isLanding || _isLanded)
        {
            _isStopping = true;
            _stopAnimation.Init();
            _currentAnimation = _stopAnimation;
            _fishingFloat.Disappear();
        }
        else
        {
            renderer.enabled = false;
            _fishingFloat.gameObject.SetActive(false);
            //_fishingFloat.Disappear();
        }

        _time = 0f;
        _isLanding = false;
        _isLanded = false;
    }

    public void Actualize(float dt)
    {
        _time += dt;

        if(_isLanding)
        {
            if(_time > _currentAnimation.duration)
            {
                _isLanding = false;
                _time = _currentAnimation.duration;
            }

            RefreshControlPoints(_time);
            RefreshLinePoints(true, true);
        }
        else if(_isStopping)
        {
            if (_time > _currentAnimation.duration)
            {
                _isStopping = false;
                //_fishingFloat.Disappear();
                renderer.enabled = false;
                _time = _currentAnimation.duration;
            }

            RefreshControlPoints(_time);
            RefreshLinePoints(true, false);
        }
        else if(_isLanded)
        {
            _to = _fishingFloat.floatPosition;

            RefreshControlPoints(_currentAnimation.duration);
            RefreshLinePoints(false, false);
        }
    }

    void RefreshControlPoints(float time)
    {
        _p0 = from;

        if (_time >= _currentAnimation.duration)
        {
            Debug.Log("auto correction");
            _p2 = _to;
        }
        else
        {
            //float p2_distance01 = _currentAnimation.distEndSpeedCurve.Evaluate(time);
            //Vector2 p2_pos2D = (_to - from) * p2_distance01 + from;

            //float p2_z01 = _currentAnimation.zEndSpeedCurve.Evaluate(time);
            //float p2_z = (_to.z - from.z) * z01 + from.z;

            //_p2 = new Vector3(pos2D.x, pos2D.y, z);

            _p2 = ComputeControlPoint(_currentAnimation.distEndSpeedCurve,
                                      _currentAnimation.zEndSpeedCurve,
                                      time,
                                      from,
                                      _to);
        }

        //float p1_distance01 = _currentAnimation.amplitudeSpeedCurve.Evaluate(time);
        //pos2D = (_p2 - from) * distance01 + from;

        //z01 = _currentAnimation.amplitudeCurve.Evaluate(time);
        //z = (_p2.z - from.z) * z01 + from.z;

        //_p1 = new Vector3(pos2D.x, pos2D.y, z);

        _p1 = ComputeControlPoint(_currentAnimation.amplitudeSpeedCurve,
                                  _currentAnimation.amplitudeCurve,
                                  time,
                                  from,
                                  _p2);
    }

    Vector3 ComputeControlPoint(EvaluationCurve posCurve,
                                EvaluationCurve zCurve, 
                                float time,
                                Vector3 from,
                                Vector3 to)
    {
        float distance01 = posCurve.Evaluate(time);
        Vector2 pos2D = (to - from) * distance01 + from;

        float z01 = zCurve.Evaluate(time);
        float z = (to.z - from.z) * z01 + from.z;

        return new Vector3(pos2D.x, pos2D.y, z);
    }

    void RefreshLinePoints(bool refreshFloatPosition, bool bufferEnd)
    {
        for (int i = 0; i < _pointCount; i++)
        {
            _points[i] = GetPoint(i / (float)(_pointCount - 1));
        }

        //_points[_pointCount - 1] = _end; 

        renderer.SetPositions(_points);

        if(refreshFloatPosition)
        {
            _fishingFloat.SetPosition(_points[_pointCount - 1]);
        }
        else
        {
            _fishingFloat.SetPosition(_end);
        }

        if (bufferEnd)
            _end = _points[_pointCount - 1];
    }

    Vector3 GetPoint(float t)
    {
        return Bezier.GetPoint(_p0, _p1, _p2, t);
    }
}
