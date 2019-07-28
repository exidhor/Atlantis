//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using UnityEngine.EventSystems;

//public class CargoCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
//{
//    public bool isOver
//    {
//        get { return _isOver; }
//    }

//    [SerializeField] Image _closeOuterCircle;
//    [SerializeField] Image _closeInnerCircle;
//    [SerializeField] Image _closeIcon;
//    [SerializeField] Button _closeButton;

//    bool _isOver;

//    void SetCloseState(bool state)
//    {
//        _closeButton.interactable = state;
//        _closeOuterCircle.enabled = state;
//        _closeInnerCircle.enabled = state;
//        _closeIcon.enabled = state;

//        _fishCount.enabled = !state
//                            && _previousView != null
//                            && !_previousView.isEmpty;
//    }

//    public void OnPointerEnter(PointerEventData eventData)
//    {
//        if (_previousView != null && !_previousView.isEmpty
//            && !PlayerControls.instance.shipInput)
//        {
//            SetCloseState(true);
//            _isOver = true;
//            CargoUI.instance.SetIsOver(true);
//        }
//    }

//    public void OnPointerExit(PointerEventData eventData)
//    {
//        if (_isOver)
//        {
//            SetCloseState(false);
//            _isOver = false;
//            CargoUI.instance.SetIsOver(false);
//        }
//    }
//}
