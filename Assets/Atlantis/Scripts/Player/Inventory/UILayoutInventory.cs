using UnityEngine;
using System.Collections.Generic;
using Tools;
using TMPro;

public class UILayoutInventory : MonoSingleton<UILayoutInventory>
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
    [SerializeField] ScaleAnim _coinsScaleAnim;
    [SerializeField] List<ShipHoldUI> _orderedHolds = new List<ShipHoldUI>();
    [SerializeField] List<CrewViewUI> _orderedCrewViews = new List<CrewViewUI>();

    bool _playerOnShipHold = false;

    void Awake()
    {
        _orderedHolds.Reverse();
        _orderedCrewViews.Reverse();

        for(int i = 0; i < _orderedCrewViews.Count; i++)
        {
            _orderedHolds[i].Hide();
        }

        for (int i = 0; i < _orderedCrewViews.Count; i++)
        {
            _orderedCrewViews[i].Hide();
        }
    }

    public InventoryCellUI GetShipHoldUI(int index)
    {
        InventoryCellUI cell = _orderedHolds[index];
        cell.Show();

        return cell;
    }

    public InventoryCellUI GetCrewViewUI(int index)
    {
        InventoryCellUI cell = _orderedCrewViews[index];
        cell.Show();

        return cell;
    }

    public void SetIsOver(bool value)
    {
        _playerOnShipHold = value;
    }

    public void RefreshCoins(int value)
    {
        _coinsText.text = value.ToString();
    }

    public void ReduceAnimCoins()
    {
        _coinsScaleAnim.Reduce();
    }

    public void GrowAnimCoins()
    {
        _coinsScaleAnim.Grow();
    }
}
