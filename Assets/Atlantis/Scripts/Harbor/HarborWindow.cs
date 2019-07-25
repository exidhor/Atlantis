using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HarborWindow : MonoBehaviour
{
    [Header("Panel Anims")]
    [SerializeField] string _inCurveName;
    [SerializeField] string _outCurveName;

    [Header("Space Anim")]
    [SerializeField] Sprite _sprite0;
    [SerializeField] Sprite _sprite1;
    [SerializeField] Image _spaceImage;

    EvaluationCurve _inCurve;
    EvaluationCurve _outCurve;

    Vector3 _posIn;
    Vector3 _posOut;

    EvaluationCurve _current;
    Vector3 _from;
    Vector3 _to;
    float _time;

    bool _isInit;

    void Awake()
    {
        Init();
        enabled = false;
    }

    void Init()
    {
        _posIn = transform.position;

        transform.position += new Vector3(Screen.width, 0f, 0f);
        _posOut = _posIn + new Vector3(Screen.width, 0f, 0f);

        _inCurve = EvaluationCurveManager.instance.GetCurve(_inCurveName);
        _outCurve = EvaluationCurveManager.instance.GetCurve(_outCurveName);

        _isInit = true;
    }

    public void SetVisible(bool visible)
    {
        if(visible)
        {
            transform.position = _posIn;
            enabled = true;
        }
        else
        {
            transform.position = _posOut;
            enabled = false;
        }
    }

    public void Appear()
    {
        if (!_isInit)
        {
            Init();
        }

        _time = 0f;
        _current = _inCurve;
        transform.position = _posOut;
        enabled = true;

        _from = _posOut;
        _to = _posIn;
    }

    public void Disappear()
    {
        if (!_isInit)
        {
            Init();
        }

        _time = 0f;
        _current = _outCurve;
        transform.position = _posIn;

        _from = _posIn;
        _to = _posOut;
    }

    void Update()
    {
        _time += Time.deltaTime;

        if(_time < _current.duration)
        {
            float ct = _current.Evaluate(_time);
            transform.position = Vector3.Lerp(_from, _to, ct);
        }
        else
        {
            SetVisible(_current == _inCurve);
        }
    }
}
