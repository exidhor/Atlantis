using UnityEngine;
using System.Collections;
using Tools;

public abstract class Harbor : QTCircleCollider
{
    #region Infos

    public abstract string rewardName
    {
        get;
    }

    public abstract Sprite genericIcon
    {
        get;
    }

    public abstract Sprite priceIcon
    {
        get;
    }

    public abstract int priceCount
    {
        get;
    }

    public abstract Sprite rewardIcon
    {
        get;
    }

    public abstract int rewardCount
    {
        get;
    }

    #endregion

    #region Animation Ids

    static readonly int ANIM_OPEN_ID = Animator.StringToHash("Open");
    static readonly int ANIM_CLOSE_ID = Animator.StringToHash("Close");
    static readonly int ANIM_IN_ID = Animator.StringToHash("In");
    static readonly int ANIM_OUT_ID = Animator.StringToHash("Out");

    #endregion

    public float innerRadius
    {
        get { return _indicator.radius; }
    }

    public Vector2 innerCircleCenter
    {
        get { return WorldConversion.ToVector2(_indicator.transform.position); }
    }

    public bool isOpen
    {
        get { return _isOpen; }
    }

    public float amountTimeLeft01
    {
        get { return _closedTime / _closeDuration; }
    }

    public abstract HarborType type
    {
        get;
    }

    [Header("Harbor")]
    [SerializeField] float _closeDuration = 30;
    [SerializeField] Gradient _colorOutside;
    [SerializeField] Gradient _colorInside;
    [SerializeField] CircleIndicator _indicator;
    [SerializeField] Animator _animator;

    bool _isOpen = true;
    float _closedTime;

    protected abstract void Refresh();
    public abstract bool AskForDeal();
    protected abstract void OnPlayerRange(bool atRange);
    protected abstract void OnOpen();
    protected abstract void OnClose();

    void Awake()
    {
        Refresh();
    }

    protected override void Update()
    {
        base.Update();

        if(!_isOpen)
        {
            _closedTime += Time.deltaTime;

            if(_closedTime > _closeDuration)
            {
                Open();
            }
        }
    }

    public void SetIndicatorVisibility(bool visible)
    {
        _indicator.SetVisible(visible);
    }

    public void SetIndicatorState(bool playerNear)
    {
        _indicator.SetColor(playerNear ? _colorInside : _colorOutside);
        OnPlayerRange(playerNear);

        if (!isOpen) return;

        if (playerNear)
        {
            _animator.SetTrigger(ANIM_IN_ID);
        }
        else
        {
            _animator.SetTrigger(ANIM_OUT_ID);
        }
    }

    public void Close()
    {
        _isOpen = false;
        _closedTime = 0f;

        _animator.SetTrigger(ANIM_CLOSE_ID);
        OnClose();
    }

    public void Open()
    {
        _isOpen = true;

        Refresh();

        _animator.SetTrigger(ANIM_OPEN_ID);
        OnOpen();
    }
}
