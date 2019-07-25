using UnityEngine;
using System.Collections;
using Tools;

public class ShipHold : MonoBehaviour
{
    public bool isEmpty
    {
        get { return _fishCount == 0; }
    }

    public FishType fishType
    {
        get { return _fishType; }
    }

    public int fishCount
    {
        get { return _fishCount; }
    }

    [SerializeField] ShipHoldUI _view;
    [SerializeField, UnityReadOnly] FishType _fishType;
    [SerializeField, UnityReadOnly] int _fishCount = 0;

    public void Fill(FishType type, int count)
    {
        _fishType = type;
        _fishCount += count;
    }

    public void Remove(int count)
    {
        _fishCount -= count;
    }

    public void RemoveAll()
    {
        _fishCount = 0;
    }
}
