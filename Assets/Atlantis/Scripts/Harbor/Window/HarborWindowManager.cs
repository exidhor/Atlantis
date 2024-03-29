﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Tools;

public class HarborWindowManager : MonoSingleton<HarborWindowManager>
{
    [Header("Panel Anims")]
    [SerializeField] string _inCurveName;
    [SerializeField] string _outCurveName;

    //[Header("Settings Global")]
    //[SerializeField] Image _fishIcon;
    //[SerializeField] Canvas _harborWindowCanvas;

    //[Header("Settings Close")]
    //[SerializeField] Canvas _closeCanvas;
    //[SerializeField] Slider _reloadSlider;

    //[Header("Settings Open")]
    //[SerializeField] Canvas _openCanvas;
    //[SerializeField] TextMeshProUGUI _fishCount;
    //[SerializeField] TextMeshProUGUI _fishPrice;

    [Header("Windows")]
    [SerializeField] HarborWindowFish _fishWindow;
    [SerializeField] HarborWindowCrew _crewWindow;

    HarborWindow _currentWindow;

    EvaluationCurve _inCurve;
    EvaluationCurve _outCurve;

    //Vector3 _posIn;
    //Vector3 _posOut;

    //EvaluationCurve _current;
    //Vector3 _from;
    //Vector3 _to;
    //float _time;

    bool _isOpen;

    bool _isInit;

    void Awake()
    {
        Init();
        //_harborWindowCanvas.enabled = false;
        enabled = false;
    }

    public void ActualizeCloseState(float amountTimeLeft)
    {
        _currentWindow.ActualizeCloseState(amountTimeLeft);
    }

    /// <summary>
    /// Return true if it needed a refresh, false otherwise
    /// </summary>
    /// <returns><c>true</c>, if open state was checked, <c>false</c> otherwise.</returns>
    public bool CheckOpenState()
    {
        return !_isOpen;
    }

    public void SetIsOpen(bool isOpen)
    {
        _isOpen = isOpen;

        _currentWindow.SetIsOpen(isOpen);
    }

    void Init()
    {
        //_posIn = transform.position;

        //transform.position += new Vector3(Screen.width, 0f, 0f);
        //_posOut = _posIn + new Vector3(Screen.width, 0f, 0f);

        _inCurve = EvaluationCurveManager.instance.GetCurve(_inCurveName);
        _outCurve = EvaluationCurveManager.instance.GetCurve(_outCurveName);

        _fishWindow.Init(_inCurve, _outCurve);
        _crewWindow.Init(_inCurve, _outCurve);

        _isInit = true;
    }

    public void SetOpenInfo(Harbor harbor)
    {
        SetIsOpen(true);

        _currentWindow.SetOpenInfo(harbor);

        //_fishIcon.sprite = icon;
        //_fishCount.text = fishCount.ToString();
        //_fishPrice.text = "+" + fishPrice;
    }

    public void SetCloseInfo(Harbor harbor)
    {
        SetIsOpen(false);

        _currentWindow.SetCloseInfo(harbor);

        //_fishIcon.sprite = icon;
    }

    public void SetVisible(bool visible)
    {
        _currentWindow.SetVisible(visible);

        if(visible)
        {
            //transform.position = _posIn;
            //_harborWindowCanvas.enabled = true;
            enabled = true;
        }
        else
        {
            //transform.position = _posOut;
            //_harborWindowCanvas.enabled = false;
            enabled = false;
        }
    }

    public void Appear(HarborType type)
    {
        if (!_isInit)
        {
            Init();
        }

        switch(type)
        {
            case HarborType.Fish:
                _currentWindow = _fishWindow;
                break;

            case HarborType.Crew:
                _currentWindow = _crewWindow;
                break;

            default:
                _currentWindow = _fishWindow;
                break;
        }

        //_harborWindowCanvas.enabled = true;
        enabled = true;

        //_time = 0f;
        //_current = _inCurve;
        //transform.position = _posOut;

        //_from = _posOut;
        //_to = _posIn;

        _currentWindow.Appear();
    }

    public void Disappear()
    {
        if (!_isInit)
        {
            Init();
        }

        //_time = 0f;
        //_current = _outCurve;
        //transform.position = _posIn;

        //_from = _posIn;
        //_to = _posOut;

        _currentWindow.Disappear();
    }

    void Update()
    {
        if(_currentWindow != null)
        {
            if(_currentWindow.Actualize(Time.deltaTime))
            {
                enabled = false;
            }
        }

        //_time += Time.deltaTime;

        //if (_time < _current.duration)
        //{
        //    float ct = _current.Evaluate(_time);
        //    transform.position = Vector3.Lerp(_from, _to, ct);
        //}
        //else
        //{
        //    SetVisible(_current == _inCurve);
        //}
    }

    public void OnRejectDeal()
    {
        _currentWindow.OnRejectDeal();
    }
}
