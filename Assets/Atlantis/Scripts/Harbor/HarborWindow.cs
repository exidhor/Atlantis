using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class HarborWindow : MonoBehaviour
{
    [Header("Panel Anims")]
    [SerializeField] string _inCurveName;
    [SerializeField] string _outCurveName;

    [Header("Settings Global")]
    [SerializeField] Image _fishIcon;
    [SerializeField] Canvas _harborWindowCanvas;

    [Header("Settings Close")]
    [SerializeField] Canvas _closeCanvas;
    [SerializeField] Slider _reloadSlider;

    [Header("Settings Open")]
    [SerializeField] Canvas _openCanvas;
    [SerializeField] TextMeshProUGUI _fishCount;
    [SerializeField] TextMeshProUGUI _fishPrice;

    EvaluationCurve _inCurve;
    EvaluationCurve _outCurve;

    Vector3 _posIn;
    Vector3 _posOut;

    EvaluationCurve _current;
    Vector3 _from;
    Vector3 _to;
    float _time;

    bool _isOpen;

    bool _isInit;

    void Awake()
    {
        Init();
        _harborWindowCanvas.enabled = false;
        enabled = false;
    }

    public void Actualize(float dt)
    {

    }

    public void SetIsOpen(bool isOpen)
    {
        _isOpen = isOpen;

        _closeCanvas.enabled = !isOpen;
        _openCanvas.enabled = isOpen;
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

    public void SetOpenState(Sprite icon, int fishCount, int fishPrice)
    {
        SetIsOpen(true);

        _fishIcon.sprite = icon;
        _fishCount.text = "-" + fishCount;
        _fishPrice.text = "+" + fishPrice;
    }

    public void SetCloseState(Sprite icon)
    {
        SetIsOpen(false);

        _fishIcon.sprite = icon;
    }

    public void SetVisible(bool visible)
    {
        if(visible)
        {
            transform.position = _posIn;
            _harborWindowCanvas.enabled = true;
            enabled = true;
        }
        else
        {
            transform.position = _posOut;
            _harborWindowCanvas.enabled = false;
            enabled = false;
        }
    }

    public void Appear()
    {
        if (!_isInit)
        {
            Init();
        }

        _harborWindowCanvas.enabled = true;
        enabled = true;

        _time = 0f;
        _current = _inCurve;
        transform.position = _posOut;

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
