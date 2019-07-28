using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

public class ShipHoldUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image _background;
    [SerializeField] Image _filled;
    [SerializeField] TextMeshProUGUI _fishCount;
    [SerializeField] Image _fishIcon;
    [SerializeField] Image _closeOuterCircle;
    [SerializeField] Image _closeInnerCircle;
    [SerializeField] Image _closeIcon;

    ShipHold _previousView;
    bool _isOver;

    void Awake()
    {
        SetCloseState(false);
    }

    void SetCloseState(bool state)
    {
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

        //_background.enabled = false;
        //_filled.enabled = false;
        //_fishCount.enabled = false;
        //_fishIcon.enabled = false;
    }

    public void Show()
    {
        gameObject.SetActive(true);

        //_background.enabled = true;
        //_filled.enabled = false;
        //_fishCount.enabled = false;
        //_fishIcon.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_previousView != null && !_previousView.isEmpty)
            SetCloseState(true);

        _isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_previousView != null && !_previousView.isEmpty)
            SetCloseState(false);

        _isOver = false;
    }

    public void OnClick()
    {

    }
}
