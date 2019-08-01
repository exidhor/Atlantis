using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using Tools;

public abstract class InventoryCellUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver
    {
        get { return _isOver; }
    }

    [Header("Displayed Infos")]
    [SerializeField] protected Image _background;
    [SerializeField] protected Image _filled;
    [SerializeField] protected Image _icon;
    [SerializeField] ScaleAnim _scaleAnim;

    [Header("Close Part")]
    [SerializeField] Image _closeOuterCircle;
    [SerializeField] Image _closeInnerCircle;
    [SerializeField] Image _closeIcon;
    [SerializeField] Button _closeButton;

    protected IShipLocation location
    {
        get { return _location; }
    }

    IShipLocation _location;
    bool _isOver;

    protected abstract void SetInformation(IShipLocation location);
    protected abstract void OnCloseState(bool state);
    protected abstract void OnSetEmpty();

    protected virtual void Awake()
    {
        SetCloseState(false);
    }

    protected void SetCloseState(bool state)
    {
        _closeButton.interactable = state;
        _closeOuterCircle.enabled = state;
        _closeInnerCircle.enabled = state;
        _closeIcon.enabled = state;

        OnCloseState(state);
    }

    public void Refresh(IShipLocation location)
    {
        _location = location;

        if (location.isEmpty)
        {
            SetEmpty();
        }
        else
        {
            _filled.enabled = true;
            _icon.enabled = true;

            SetInformation(location);
        }
    }

    protected virtual void SetEmpty()
    {
        _filled.enabled = false;
        _icon.enabled = false;

        _background.color = UILayoutInventory.instance.emptyHoldColor;

        OnSetEmpty();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_location != null && !_location.isEmpty
            && !PlayerControls.instance.shipInput)
        {
            SetCloseState(true);
            _isOver = true;
            UILayoutInventory.instance.SetIsOver(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isOver)
        {
            SetCloseState(false);
            _isOver = false;
            UILayoutInventory.instance.SetIsOver(false);
        }
    }

    public void OnClick()
    {
        _location.OnClick();
        Refresh(_location);
        SetCloseState(false);
    }

    public void DoGrowAnim(bool doDelay = true)
    {
        _scaleAnim.Grow(doDelay ? UILayoutInventory.instance.delayReceiving : 0f);
    }

    public void DoReduceAnim(bool doDelay = true)
    {
        _scaleAnim.Reduce(doDelay ? UILayoutInventory.instance.delayPaying : 0f);
    }
}
