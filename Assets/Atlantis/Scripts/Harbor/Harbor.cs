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

    [Header("Harbor")]
    [SerializeField] Gradient _colorOutside;
    [SerializeField] Gradient _colorInside;
    [SerializeField] CircleIndicator _indicator;

    FishType _fishType;
    Sprite _fishIcon;
    int _fishPrice;
    int _fishCount;

    void Awake()
    {
        Refresh();
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
        window.Set(_fishIcon, _fishCount, _fishPrice * _fishCount);
    }

    public void SetIndicatorVisibility(bool visible)
    {
        _indicator.SetVisible(visible);
    }

    public void SetIndicatorState(bool playerNear)
    {
        _indicator.SetColor(playerNear ? _colorInside : _colorOutside);
    }
}
