using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Tools;

public class Cargo : MonoSingleton<Cargo>
{
    [Serializable]
    class FishCounter
    {
        public int count
        {
            get { return _fishCount; }
        }

        [SerializeField] Text _countText;
        [SerializeField] Sprite _spriteEnable;
        [SerializeField] Image _image;
        int _fishCount;

        public void Add(int count)
        {
            _fishCount += count;
            _countText.text = _fishCount.ToString() + "/1";

            _image.sprite = _spriteEnable;
        }
    }

    [SerializeField] FishCounter _littleFish;
    [SerializeField] FishCounter _bigFish;
    [SerializeField] FishCounter _octopus;

    [SerializeField] int _holdCount = 4;

    [SerializeField]
    List<ShipHold> _poolHolds = new List<ShipHold>();

    [SerializeField, UnityReadOnly]
    List<ShipHold> _holds = new List<ShipHold>();

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

        //switch(type)
        //{
        //    case FishType.bigFish:
        //        _bigFish.Add(count);
        //        break;

        //    case FishType.littleFish:
        //        _littleFish.Add(count);
        //        break;

        //    case FishType.octopus:
        //        _octopus.Add(count);
        //        break;
        //}

        //CheckForWin();
    }

    ShipHold FindHold(FishType type)
    {
        for(int i = 0; i < _holds.Count; i++)
        {
            if(!_holds[i].isEmpty 
                && _holds[i].fishType == type
                && !_holds[i].isFull)
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

    void CheckForWin()
    {
        if(_littleFish.count > 0 
            && _bigFish.count > 0
            && _octopus.count > 0)
        {
            MainManager.instance.EndGame();
        }
    }

    public bool CanStore(FishType type)
    {
        for(int i = 0; i < _holds.Count; i++)
        {
            if(_holds[i].isEmpty || 
                (_holds[i].fishType == type && !_holds[i].isFull))
            {
                return true;
            }
        }

        return false;
    }
}
