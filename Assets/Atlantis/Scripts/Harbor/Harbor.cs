using UnityEngine;
using System.Collections;
using Tools;

public abstract class Harbor : QTCircleCollider
{
    public float innerRadius
    {
        get { return _indicator.radius; }
    }

    public Vector2 innerCircleCenter
    {
        get { return WorldConversion.ToVector2(_indicator.transform.position); }
    }

    public bool isOpen
    {
        get { return _isOpen; }
    }

    public float amountTimeLeft01
    {
        get { return _closedTime / _closeDuration; }
    }

    [Header("Harbor")]
    [SerializeField] float _closeDuration = 30;
    [SerializeField] Gradient _colorOutside;
    [SerializeField] Gradient _colorInside;
    [SerializeField] CircleIndicator _indicator;

    bool _isOpen = true;
    float _closedTime;

    protected abstract void Refresh();
    public abstract void SetHarborWindow(HarborWindowManager window);
    public abstract bool AskForDeal();

    void Awake()
    {
        Refresh();
    }

    protected override void Update()
    {
        base.Update();

        if(!_isOpen)
        {
            _closedTime += Time.deltaTime;

            if(_closedTime > _closeDuration)
            {
                Open();
            }
        }
    }

    public void SetIndicatorVisibility(bool visible)
    {
        _indicator.SetVisible(visible);
    }

    public void SetIndicatorState(bool playerNear)
    {
        _indicator.SetColor(playerNear ? _colorInside : _colorOutside);
    }

    public void Close()
    {
        _isOpen = false;
        _closedTime = 0f;
        //_enable = false;
    }

    public void Open()
    {
        _isOpen = true;
        //_enable = true;

        Refresh();
    }
}
