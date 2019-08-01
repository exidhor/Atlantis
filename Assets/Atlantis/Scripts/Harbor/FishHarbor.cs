using UnityEngine;
using System.Collections;

public class FishHarbor : Harbor
{
    #region Infos

    public override string rewardName
    {
        get { throw new System.NotImplementedException(); }
    }

    public override Sprite genericIcon
    {
        get { return FishLibrary.instance.genericFishIcon; }
    }

    public override Sprite priceIcon
    {
        get { return _fishIcon; }
    }

    public override int priceCount
    {
        get { return _fishCount; }
    }

    public override Sprite rewardIcon
    {
        get { throw new System.NotImplementedException(); }
    }

    public override int rewardCount
    {
        get { return _fishPrice * _fishCount; }
    }

    #endregion

    public override HarborType type
    {
        get { return HarborType.Fish; }
    }

    FishType _fishType;
    Sprite _fishIcon;
    int _fishPrice;
    int _fishCount;

    protected override void Refresh()
    {
        FishInfo info = FishLibrary.instance.GetRandomFish();

        _fishType = info.type;
        _fishIcon = info.icon;
        _fishPrice = info.GetRandomPrice();
        _fishCount = info.GetRandomCount();
    }

    public override bool AskForDeal()
    {
        if (Cargo.instance.CanPayFish(_fishType, _fishCount))
        {
            Cargo.instance.PayFish(_fishType, _fishCount);
            Cargo.instance.ReceiveCoins(_fishPrice * _fishCount);
            return true;
        }

        return false;
    }
}
