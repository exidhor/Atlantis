using UnityEngine;
using System.Collections;

public class CrewHarbor : Harbor
{
    #region Infos

    public override string rewardName
    {
        get { return _crewName; }
    }

    public override Sprite genericIcon
    {
        get { throw new System.NotImplementedException(); }
    }

    public override Sprite priceIcon
    {
        get { throw new System.NotImplementedException(); }
    }

    public override int priceCount
    {
        get { return _crewPrice; }
    }

    public override Sprite rewardIcon
    {
        get { return _crewIcon; }
    }

    public override int rewardCount
    {
        get { return 1; }
    }

    #endregion

    public override HarborType type
    {
        get { return HarborType.Crew; }
    }

    string _crewName;
    CrewType _crewType;
    Sprite _crewIcon;
    int _crewPrice;

    protected override void Refresh()
    {
        // tmp
        _crewType = CrewType.Fisherman;

        CrewInfo info = CrewLibrary.instance.GetInfo(_crewType);
        _crewName = info.name;
        _crewPrice = info.GetRandomPrice();
        _crewIcon = info.icon;
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

    protected override void OnPlayerRange(bool atRange)
    {
        // todo
    }

    protected override void OnOpen()
    {
        // todo
    }

    protected override void OnClose()
    {
        // todo
    }
}
