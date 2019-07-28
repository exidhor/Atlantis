using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

public class ShipHoldUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver
    {
        get { return _isOver; }
    }

    [SerializeField] Image _background;
    [SerializeField] Image _filled;
    [SerializeField] TextMeshProUGUI _fishCount;
    [SerializeField] Image _fishIcon;
    [SerializeField] Image _closeOuterCircle;
    [SerializeField] Image _closeInnerCircle;
    [SerializeField] Image _closeIcon;
    [SerializeField] Button _closeButton;

    ShipHold _previousView;
    bool _isOver;

    void Awake()
    {
        SetCloseState(false);
    }

    void SetCloseState(bool state)
    {
        _closeButton.interactable = state;
        _closeOuterCircle.enabled = state;
        _closeInnerCircle.enabled = state;
        _closeIcon.enabled = state;

        _fishCount.enabled = !state 
                            && _previousView != null 
                            && !_previousView.isEmpty;
    }

    public void Refresh(ShipHold hold)
    {
        _previousView = hold;

        if (hold.isEmpty)
        {
            _filled.enabled = false;
            _fishCount.enabled = false;
            _fishIcon.enabled = false;

            _background.color = CargoUI.instance.emptyHoldColor;
        }
        else
        {
            _filled.enabled = true;
            _fishCount.enabled = !_isOver;
            _fishIcon.enabled = true;

            FishInfo info = FishLibrary.instance.GetFishInfo(hold.fishType);

            _background.color = info.holdBackgroundColor;
            _filled.color = info.holdFrontColor;
            _filled.fillAmount = hold.fishCount / (float)hold.capacity;
            _fishCount.text = hold.fishCount.ToString();
            _fishIcon.sprite = info.icon;
        }
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
        if(_previousView != null && !_previousView.isEmpty 
            && !PlayerControls.instance.shipInput)
        {
            SetCloseState(true);
            _isOver = true;
            CargoUI.instance.SetIsOver(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isOver)
        {
            SetCloseState(false);
            _isOver = false;
            CargoUI.instance.SetIsOver(false);
        }
    }

    public void OnClick()
    {
        _previousView.RemoveAll();
        Refresh(_previousView);
        SetCloseState(false);
    }
}
