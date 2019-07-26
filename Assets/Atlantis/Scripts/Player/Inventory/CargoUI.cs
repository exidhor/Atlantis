using UnityEngine;
using System.Collections.Generic;
using Tools;

public class CargoUI : MonoSingleton<CargoUI>
{
    public Color emptyHoldColor
    {
        get { return _emptyHoldColor; }
    }

    [SerializeField] Color _emptyHoldColor;

    [SerializeField] List<ShipHoldUI> _orderedHolds = new List<ShipHoldUI>();

    void Awake()
    {
        for(int i = 0; i < _orderedHolds.Count; i++)
        {
            _orderedHolds[i].Hide();
        }
    }

    public ShipHoldUI GetShipHoldUI(int index)
    {
        ShipHoldUI holdUI = _orderedHolds[index];
        holdUI.Show();

        return holdUI;
    }
}
