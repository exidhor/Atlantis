using UnityEngine;
using System;
using System.Collections.Generic;
using Tools;

public class FishLibrary : MonoSingleton<FishLibrary>
{
    [SerializeField] List<FishInfo> _fishes = new List<FishInfo>();

    void Awake()
    {
        _fishes.Sort((a, b) => a.type.CompareTo(b.type));
    }

    public FishInfo GetFishInfo(FishType type)
    {
        return _fishes[(int)type];
    }
}
