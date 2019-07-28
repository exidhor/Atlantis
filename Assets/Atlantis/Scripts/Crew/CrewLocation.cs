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
        get { return false; }
    }

    Crew _current = null;
    InventoryCellUI _view;

    public void SetCurrent(Crew crew, int index)
    {
        _current = crew;

        if (current != null)
        {
            current.transform.SetParent(transform, false);
            _view = UILayoutInventory.instance.GetCrewViewUI(index);
            _view.Refresh(this);
        }
    }

    public void OnClick()
    {
        // todo
    }
}
