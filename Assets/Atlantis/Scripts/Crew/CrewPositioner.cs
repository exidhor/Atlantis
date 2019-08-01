using UnityEngine;
using System.Collections.Generic;
using Tools;

public class CrewPositioner : MonoBehaviour
{
    public int freePositions
    {
        get { return _freePositions; }
    }

    [SerializeField]
    List<CrewLocation> _poolPositions = new List<CrewLocation>();

    [SerializeField, UnityReadOnly] 
    List<CrewLocation> _positions = new List<CrewLocation>();

    int _freePositions;

    //void Awake()
    //{
    //    _freePositions = _positions.Count;
    //}

    public void SetPositionCount(int count)
    {
        _freePositions = count;

        for(int i = 0; i < count; i++)
        {
            _poolPositions[i].SetCurrent(null, i, false);
            _positions.Add(_poolPositions[i]);
        }
    }

    public Transform SetPosition(Crew crew, bool doAnim)
    {
        for(int i = 0; i < _positions.Count; i++)
        {
            if(_positions[i].available)
            {
                _freePositions--;
                _positions[i].SetCurrent(crew, i, doAnim);

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
                _positions[i].SetCurrent(null, i, true);
                return;
            }
        }
    }

    public void VirtualRelease()
    {
        _freePositions++;
    }
}
