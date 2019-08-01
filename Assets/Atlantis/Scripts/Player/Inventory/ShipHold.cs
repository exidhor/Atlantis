using UnityEngine;
using System.Collections;
using Tools;

public class ShipHold : MonoBehaviour, IShipLocation
{
    public bool isEmpty
    {
        get { return _fishCount == 0; }
    }

    public bool isFull
    {
        get { return _fishCount == capacity; }
    }

    public FishType fishType
    {
        get { return _fishType; }
    }

    public int fishCount
    {
        get { return _fishCount; }
    }

    public int capacity
    {
        get { return _capacity; }
    }

    [SerializeField] int _capacity = 50;
    [SerializeField, UnityReadOnly] InventoryCellUI _view;
    [SerializeField, UnityReadOnly] FishType _fishType;
    [SerializeField, UnityReadOnly] int _fishCount = 0;

    public void Init(int index)
    {
        _view = UILayoutInventory.instance.GetShipHoldUI(index);
        _view.Refresh(this);
    }

    public void Fill(FishType type, int count)
    {
        _fishType = type;
        _fishCount += count;

        RefreshShipHoldView();
        _view.DoGrowAnim(false);
    }

    public bool Contains(FishType type)
    {
        return !isEmpty && fishType == type;
    }

    public void Remove(int count)
    {
        _fishCount -= count;

        RefreshShipHoldView();
        _view.DoReduceAnim(true);
    }

    public void RemoveAll()
    {
        _fishCount = 0;

        RefreshShipHoldView();
        _view.DoReduceAnim(false);
    }

    void RefreshShipHoldView()
    {
        _view.Refresh(this);
    }

    void IShipLocation.OnClick()
    {
        RemoveAll();
    }
}
