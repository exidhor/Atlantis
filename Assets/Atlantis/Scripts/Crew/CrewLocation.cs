using UnityEngine;
using System.Collections;

public class CrewLocation : MonoBehaviour, IShipLocation
{
    public bool available
    {
        get { return current == null; }
    }

    public Crew current
    {
        get { return _current; }
    }

    public bool isEmpty
    {
        get { return _current == null; }
    }

    Crew _current = null;
    InventoryCellUI _view;

    public void SetCurrent(Crew crew, int index, bool doAnim)
    {
        _current = crew;

        if(current != null)
        {
            current.transform.SetParent(transform, false);
        }

        _view = UILayoutInventory.instance.GetCrewViewUI(index);
        _view.Refresh(this);

        if(doAnim)
        {
            if(current != null)
            {
                _view.DoGrowAnim(true);
            }
            else
            {
                _view.DoReduceAnim(false);
            }
        }
    }

    void LateUpdate()
    {
        if(_current != null)
        {
            _view.Refresh(this);
        }
    }

    public void OnClick()
    {
        RemoveCrew();
    }

    void RemoveCrew()
    {
        current.Release();
        _current = null;
        _view.Refresh(this);

        _view.DoReduceAnim(false);
    }
}
