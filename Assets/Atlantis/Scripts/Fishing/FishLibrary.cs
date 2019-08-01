using UnityEngine;
using System;
using System.Collections.Generic;
using Tools;

public class FishLibrary : MonoSingleton<FishLibrary>
{
    public Sprite genericFishIcon
    {
        get { return _genericFishIcon; }
    }

    [SerializeField] Sprite _genericFishIcon;
    [SerializeField] List<FishInfo> _fishes = new List<FishInfo>();

    void Awake()
    {
        _fishes.Sort((a, b) => a.type.CompareTo(b.type));

        //for(int i = 0; i < _fishes.Count; i++)
        //{
        //    _fishes[i].Init();
        //}
    }

    public FishInfo GetFishInfo(FishType type)
    {
        return _fishes[(int)type];
    }

    public FishInfo GetRandomFish()
    {
        int index = UnityEngine.Random.Range(0, _fishes.Count);

        return _fishes[index];
    }
}
