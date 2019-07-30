using UnityEngine;
using System.Collections;

public class CrewViewUI : InventoryCellUI
{
    CrewLocation crewLocation
    {
        get { return (CrewLocation)location; }
    }

    protected override void OnCloseState(bool state)
    {
        // nothing
    }

    protected override void OnSetEmpty()
    {
        // nothing
    }

    protected override void SetInformation(IShipLocation location)
    {
        Crew crew = crewLocation.current;

        CrewInfo info = CrewLibrary.instance.GetInfo(crew.type);

        _background.color = info.backgroundColor;
        _filled.color = info.frontColor;
        _filled.fillAmount = crew.progress01;
        _icon.sprite = info.icon;
    }
}
