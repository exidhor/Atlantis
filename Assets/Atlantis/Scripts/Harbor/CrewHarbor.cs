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

    [SerializeField] MeshRenderer _flagRenderer;
    [SerializeField] Animator _animator;

    string _crewName;
    CrewType _crewType;
    Sprite _crewIcon;
    int _crewPrice;

    protected override void Refresh()
    {
        CrewInfo info = CrewLibrary.instance.GetRandomInfo();

        _crewType = info.model.type;
        _crewName = info.name;
        _crewPrice = info.GetRandomPrice();
        _crewIcon = info.icon;
        _flagRenderer.material.SetTexture("Texture2D_D003A093", _crewIcon.texture);
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
        if (atRange)
        {
            _animator.SetTrigger("In");
        }
        else
        {
            _animator.SetTrigger("Out");
        }
    }

    protected override void OnOpen()
    {
        _animator.SetTrigger("Open");
    }

    protected override void OnClose()
    {
        _flagRenderer.material.SetTexture("Texture2D_D003A093",
                                          CrewLibrary.instance.genericCloseIcon.texture);

        _animator.SetTrigger("Close");
    }
}
