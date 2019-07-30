using UnityEngine;
using System.Collections.Generic;
using System;
using Tools;

public class CrewShipManager : MonoSingleton<CrewShipManager>
{
    [Serializable]
    class StartingCrew
    {
        public CrewType type;
        public int count = 1;
    }

    public int freePositions
    {
        get { return _positioner.freePositions; }
    }

    [Header("Starting")]
    [SerializeField] int _maxShipSize = 4;
    [SerializeField]
    List<StartingCrew> _startingCrews = new List<StartingCrew>();

    [Header("Linking")]
    [SerializeField] CrewPositioner _positioner;

    void Awake()
    {
        _positioner.SetPositionCount(_maxShipSize);

        for(int i = 0; i < _startingCrews.Count; i++)
        {
            for(int c = 0; c < _startingCrews[i].count; c++)
            {
                AddCrew(_startingCrews[i].type);
            }
        }
    }

    public void AddCrew(CrewType type)
    {
        Crew crew = CrewLibrary.instance.GetFreeCrew(type);
        _positioner.SetPosition(crew);
    }
}
