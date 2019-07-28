using UnityEngine;
using System.Collections.Generic;
using System;

public class CrewShipManager : MonoBehaviour
{
    [Serializable]
    class StartingCrew
    {
        public CrewType type;
        public int count = 1;
    }

    [Header("Starting")]
    [SerializeField]
    List<StartingCrew> _startingCrews = new List<StartingCrew>();

    [Header("Linking")]
    [SerializeField] CrewPositioner _positioner;

    void Awake()
    {
        for(int i = 0; i < _startingCrews.Count; i++)
        {
            for(int c = 0; c < _startingCrews[i].count; c++)
            {
                Crew crew = CrewLibrary.instance.GetFreeCrew(_startingCrews[i].type);
                _positioner.SetPosition(crew);
            }
        }
    }
}
