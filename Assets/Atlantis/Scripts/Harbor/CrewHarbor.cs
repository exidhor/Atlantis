using UnityEngine;
using System.Collections;

public class CrewHarbor : Harbor
{
    CrewType _crewType;
    Sprite _crewIcon;
    int _crewPrice;

    protected override void Refresh()
    {
        // tmp
        _crewType = CrewType.Fisherman;

        CrewInfo info = CrewLibrary.instance.GetInfo(_crewType);
        _crewPrice = info.GetRandomPrice();
        _crewIcon = info.icon;
    }

    public override void SetHarborWindow(HarborWindow window)
    {
        if (isOpen)
        {
            window.SetOpenState(_crewIcon, 1, _crewPrice);
        }
        else
        {
            window.SetCloseState(CrewLibrary.instance.genericCrewIcon);
        }
    }

    public override bool AskForDeal()
    {
        if (Cargo.instance.CanPayCoins(_crewPrice)
            && CrewShipManager.instance.freePositions > 0)
        {
            Cargo.instance.PayCoins(_crewPrice);
            CrewShipManager.instance.AddCrew(_crewType);
            return true;
        }

        return false;
    }
}
