using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public abstract class HarborWindow : MonoBehaviour
{
    [Header("Generics")]
    [SerializeField] Canvas _globalCanvas;
    [SerializeField] Canvas _openCanvas;
    [SerializeField] Canvas _closeCanvas;
    [SerializeField] Slider _reloadSlider;
    [SerializeField] Vibrator _vibrator;

    EvaluationCurve _inCurve;
    EvaluationCurve _outCurve;

    Vector3 _posIn;
    Vector3 _posOut;

    EvaluationCurve _current;
    Vector3 _from;
    Vector3 _to;
    float _time;

    bool _isInit;

    public abstract void SetOpenInfo(Harbor harbor);

    public void SetCloseInfo(Harbor harbor)
    {
        // nothing
    }

    public void Init(EvaluationCurve inCurve, EvaluationCurve outCurve)
    {
        _posIn = transform.position;

        transform.position += new Vector3(Screen.width, 0f, 0f);
        _posOut = _posIn + new Vector3(Screen.width, 0f, 0f);

        _inCurve = inCurve;
        _outCurve = outCurve;

        _isInit = true;
    }

    public void Appear()
    {
        _globalCanvas.enabled = true;

        _time = 0f;
        _current = _inCurve;
        transform.position = _posOut;

        _from = _posOut;
        _to = _posIn;
    }

    public void Disappear()
    {
        _time = 0f;
        _current = _outCurve;
        transform.position = _posIn;

        _from = _posIn;
        _to = _posOut;
    }

    public void ActualizeCloseState(float amountTimeLeft)
    {
        _reloadSlider.value = amountTimeLeft;
    }

    public bool Actualize(float dt)
    {
        _time += dt;

        if (_time < _current.duration)
        {
            float ct = _current.Evaluate(_time);
            transform.position = Vector3.Lerp(_from, _to, ct);
            return false;
        }
        else
        {
            SetVisible(_current == _inCurve);
            return _current == _outCurve;
        }
    }

    public void SetIsOpen(bool isOpen)
    {
        _closeCanvas.enabled = !isOpen;
        _openCanvas.enabled = isOpen;
    }

    public void SetVisible(bool visible)
    {
        if (visible)
        {
            transform.position = _posIn;
            _globalCanvas.enabled = true;
        }
        else
        {
            transform.position = _posOut;
            _globalCanvas.enabled = false;
        }
    }

    public void OnRejectDeal()
    {
        _vibrator.Vibrate();
    }
}
