using UnityEngine;
using System.Collections;
using Tools;

public class Harbor : QTCircleCollider
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

    FishType _fishType;
    Sprite _fishIcon;
    int _fishPrice;
    int _fishCount;

    bool _isOpen = true;
    float _closedTime;

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

    void Refresh()
    {
        FishInfo info = FishLibrary.instance.GetRandomFish();

        _fishType = info.type;
        _fishIcon = info.icon;
        _fishPrice = info.GetRandomPrice();
        _fishCount = info.GetRandomCount();
    }

    public void SetHarborWindow(HarborWindow window)
    {
        if(_isOpen)
        {
            window.SetOpenState(_fishIcon, _fishCount, _fishPrice * _fishCount);
        }
        else
        {
            window.SetCloseState(FishLibrary.instance.genericFishIcon);
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

    public bool AskForDeal()
    {
        if(Cargo.instance.CanPay(_fishType, _fishCount))
        {
            Cargo.instance.Pay(_fishType, _fishCount);
            Cargo.instance.ReceiveCoins(_fishPrice * _fishCount);
            return true;
        }

        return false;
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
