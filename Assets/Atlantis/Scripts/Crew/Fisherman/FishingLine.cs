using UnityEngine;
using System;
using Tools;

[RequireComponent(typeof(LineRenderer))]
public class FishingLine : MonoBehaviour
{
    [Serializable]
    class Animation
    {
        public float duration;
        [SerializeField] string _xzLineName;
        [SerializeField] string _zLineName;

        [SerializeField] string _xzCurveName;
        [SerializeField] string _yCurveName;

        [NonSerialized] public EvaluationCurve xzLine;
        [NonSerialized] public EvaluationCurve yLine;
        [NonSerialized] public EvaluationCurve xzCurve;
        [NonSerialized] public EvaluationCurve yCurve;

        public void Init()
        {
            if (xzLine == null)
            {
                xzLine = EvaluationCurveManager.instance.GetCurve(_xzLineName);
                yLine = EvaluationCurveManager.instance.GetCurve(_zLineName);
                xzCurve = EvaluationCurveManager.instance.GetCurve(_xzCurveName);
                yCurve = EvaluationCurveManager.instance.GetCurve(_yCurveName);
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

    //EvaluationCurve _distEndSpeedCurve;
    //EvaluationCurve _zEndSpeedCurve;
    //EvaluationCurve _amplitudeCurve;
    //EvaluationCurve _amplitudeSpeedCurve;

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
            //Debug.Log("auto correction");
            _p2 = _to;
        }
        else
        {
            //float p2_distance01 = _currentAnimation.distEndSpeedCurve.Evaluate(time);
            //Vector2 p2_pos2D = (_to - from) * p2_distance01 + from;

            //float p2_z01 = _currentAnimation.zEndSpeedCurve.Evaluate(time);
            //float p2_z = (_to.z - from.z) * z01 + from.z;

            //_p2 = new Vector3(pos2D.x, pos2D.y, z);

            _p2 = ComputeControlPoint(_currentAnimation.xzLine,
                                      _currentAnimation.yLine,
                                      time,
                                      from,
                                      _to);
        }

        //float p1_distance01 = _currentAnimation.amplitudeSpeedCurve.Evaluate(time);
        //pos2D = (_p2 - from) * distance01 + from;

        //z01 = _currentAnimation.amplitudeCurve.Evaluate(time);
        //z = (_p2.z - from.z) * z01 + from.z;

        //_p1 = new Vector3(pos2D.x, pos2D.y, z);

        _p1 = ComputeControlPoint(_currentAnimation.yCurve,
                                  _currentAnimation.xzCurve,
                                  time,
                                  from,
                                  _p2);
    }

    Vector3 ComputeControlPoint(EvaluationCurve xyCurve,
                                EvaluationCurve yCurve, 
                                float time,
                                Vector3 from,
                                Vector3 to)
    {
        Vector2 from2D = WorldConversion.ToVector2(from);
        Vector2 to2D = WorldConversion.ToVector2(to);

        float distance01 = xyCurve.Evaluate(time);
        Vector2 pos2D = (to2D - from2D) * distance01 + from2D;

        float y01 = yCurve.Evaluate(time);
        float y = (to.y - from.y) * y01 + from.y;

        return new Vector3(pos2D.x, y, pos2D.y);
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
