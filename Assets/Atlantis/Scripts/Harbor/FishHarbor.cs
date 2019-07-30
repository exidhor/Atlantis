using UnityEngine;
using System.Collections;

public class FishHarbor : Harbor
{
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

    public override void SetHarborWindow(HarborWindow window)
    {
        if (isOpen)
        {
            window.SetOpenState(_fishIcon, _fishCount, _fishPrice * _fishCount);
        }
        else
        {
            window.SetCloseState(FishLibrary.instance.genericFishIcon);
        }
    }

    public override bool AskForDeal()
    {
        if (Cargo.instance.CanPay(_fishType, _fishCount))
        {
            Cargo.instance.Pay(_fishType, _fishCount);
            Cargo.instance.ReceiveCoins(_fishPrice * _fishCount);
            return true;
        }

        return false;
    }
}
