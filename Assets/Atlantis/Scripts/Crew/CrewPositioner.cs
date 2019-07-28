using UnityEngine;
using System.Collections.Generic;

public class CrewPositioner : MonoBehaviour
{
    class CrewPosition
    {
        public bool available
        {
            get { return current == null; }
        }

        public Crew current
        {
            get { return _current; }
        }

        public Transform transform;

        Crew _current = null;

        public CrewPosition(Transform tr)
        {
            transform = tr;
        }

        public void SetCurrent(Crew crew)
        {
            _current = crew;

            if(current != null)
            {
                current.transform.SetParent(transform, false);
            }
        }
    }

    public int freePositions
    {
        get { return _freePositions; }
    }

    [SerializeField] 
    List<Transform> _positions = new List<Transform>();

    int _freePositions;
    List<CrewPosition> _crewPositions = new List<CrewPosition>();

    void Awake()
    {
        for(int i = 0; i < _positions.Count; i++)
        {
            _crewPositions.Add(new CrewPosition(_positions[i]));
        }

        _freePositions = _crewPositions.Count;
    }

    public Transform SetPosition(Crew crew)
    {
        for(int i = 0; i < _crewPositions.Count; i++)
        {
            if(_crewPositions[i].available)
            {
                _freePositions--;
                _crewPositions[i].SetCurrent(crew);

                return _crewPositions[i].transform;
            }
        }

        return null;
    }

    public void ReleasePosition(Crew crew)
    {
        for(int i = 0; i < _crewPositions.Count; i++)
        {
            if(_crewPositions[i].current == crew)
            {
                _freePositions++;
                _crewPositions[i].SetCurrent(null);
                return;
            }
        }
    }
}
