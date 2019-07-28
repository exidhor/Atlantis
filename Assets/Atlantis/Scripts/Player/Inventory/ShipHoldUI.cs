using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

public class ShipHoldUI : InventoryCellUI
{
    [Header("Ship Hold Variables")]
    [SerializeField] TextMeshProUGUI _fishCount;

    ShipHold hold
    {
        get { return (ShipHold)location; }
    }

    protected override void OnSetEmpty()
    {
        _fishCount.enabled = false;
    }

    protected override void OnCloseState(bool state)
    {
        _fishCount.enabled = !state
                    && location != null
                    && !location.isEmpty;
    }

    protected override void SetInformation(IShipLocation location)
    {
        _fishCount.enabled = !isOver;

        FishInfo info = FishLibrary.instance.GetFishInfo(hold.fishType);

        _background.color = info.holdBackgroundColor;
        _filled.color = info.holdFrontColor;
        _filled.fillAmount = hold.fishCount / (float)hold.capacity;
        _fishCount.text = hold.fishCount.ToString();
        _icon.sprite = info.icon;
    }
}
