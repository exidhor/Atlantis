using UnityEngine;
using System.Collections.Generic;

public class CrewPositioner : MonoBehaviour
{
    public int freePositions
    {
        get { return _freePositions; }
    }

    [SerializeField] 
    List<CrewLocation> _positions = new List<CrewLocation>();

    int _freePositions;

    void Awake()
    {
        _freePositions = _positions.Count;
    }

    public Transform SetPosition(Crew crew)
    {
        for(int i = 0; i < _positions.Count; i++)
        {
            if(_positions[i].available)
            {
                _freePositions--;
                _positions[i].SetCurrent(crew, i);

                return _positions[i].transform;
            }
        }

        return null;
    }

    public void ReleasePosition(Crew crew)
    {
        for(int i = 0; i < _positions.Count; i++)
        {
            if(_positions[i].current == crew)
            {
                _freePositions++;
                _positions[i].SetCurrent(null, i);
                return;
            }
        }
    }
}
