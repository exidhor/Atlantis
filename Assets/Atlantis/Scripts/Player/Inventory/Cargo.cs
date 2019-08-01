using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Tools;

public class Cargo : MonoSingleton<Cargo>
{
    public int coinCount
    {
        get { return coinCount; }
    }

    [SerializeField] int _holdCount = 4;
    [SerializeField] int _startingCoins = 0;

    [SerializeField]
    List<ShipHold> _poolHolds = new List<ShipHold>();

    [SerializeField, UnityReadOnly]
    List<ShipHold> _holds = new List<ShipHold>();

    int _coinCount;

    void Awake()
    {
        for(int i = 0; i < _poolHolds.Count; i++)
        {
            if(i < _holdCount)
            {
                _holds.Add(_poolHolds[i]);
                _poolHolds[i].gameObject.SetActive(true);
                _poolHolds[i].Init(i);
            }
            else
            {
                _poolHolds[i].gameObject.SetActive(false);
            }
        }

        _coinCount = _startingCoins;
        UILayoutInventory.instance.RefreshCoins(_coinCount);
    }

    public void AddFish(FishType type, int count)
    {
        ShipHold found = FindHold(type);

        if(found == null)
        {
            found = GetFirstEmpty();

            if(found == null)
            {
                Debug.Log("Everything is full !!!");
            }
        }

        if(found != null)
        {
            found.Fill(type, count);
        }
    }

    ShipHold FindHold(FishType type)
    {
        for(int i = 0; i < _holds.Count; i++)
        {
            if(_holds[i].Contains(type) && !_holds[i].isFull)
            {
                return _holds[i];
            }
        }

        return null;
    }

    ShipHold GetFirstEmpty()
    {
        for(int i = 0; i < _holds.Count; i++)
        {
            if(_holds[i].isEmpty)
            {
                return _holds[i];
            }
        }

        return null;
    }

    public bool CanStore(FishType type)
    {
        for(int i = 0; i < _holds.Count; i++)
        {
            if(_holds[i].isEmpty
            || (_holds[i].fishType == type && !_holds[i].isFull))
            {
                return true;
            }
        }

        return false;
    }

    public bool CanPayFish(FishType type, int count)
    {
        int owned = 0;

        for(int i = 0; i < _holds.Count; i++)
        {
            if(_holds[i].Contains(type))
            {
                owned += _holds[i].fishCount;
            }
        }

        return owned >= count;
    }

    public bool CanPayCoins(int count)
    {
        return count <= _coinCount;
    }

    public void PayFish(FishType type, int count)
    {
        int paid = 0;

        for (int i = _holds.Count - 1; i >= 0; i--)
        {
            ShipHold hold = _holds[i];

            if (hold.Contains(type))
            {
                if(hold.fishCount + paid >= count)
                {
                    hold.Remove(count - paid);
                    return;
                }
                else
                {
                    paid += hold.fishCount;
                    hold.RemoveAll();
                }
            }
        }
    }

    public void PayCoins(int count)
    {
        _coinCount -= count;

        UILayoutInventory.instance.RefreshCoins(_coinCount);
        UILayoutInventory.instance.ReduceAnimCoins();
    }

    public void ReceiveCoins(int count)
    {
        _coinCount += count;

        UILayoutInventory.instance.RefreshCoins(_coinCount);
        UILayoutInventory.instance.GrowAnimCoins();
    }
}
