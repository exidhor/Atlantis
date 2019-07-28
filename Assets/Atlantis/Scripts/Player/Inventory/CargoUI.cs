using UnityEngine;
using System.Collections.Generic;
using Tools;
using TMPro;

public class CargoUI : MonoSingleton<CargoUI>
{
    public Color emptyHoldColor
    {
        get { return _emptyHoldColor; }
    }

    public bool playerOnShipHold
    {
        get { return _playerOnShipHold; }
    }

    [Header("Values")]
    [SerializeField] Color _emptyHoldColor;

    [Header("Linking")]
    [SerializeField] TextMeshProUGUI _coinsText;
    [SerializeField] List<ShipHoldUI> _orderedHolds = new List<ShipHoldUI>();

    bool _playerOnShipHold = false;

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

    public void SetIsOver(bool value)
    {
        _playerOnShipHold = value;
    }

    public void RefreshCoins(int value)
    {
        _coinsText.text = value.ToString();
    }
}
