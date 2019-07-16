using UnityEngine;
using System.Collections;

public class PlayerInputFeedback : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] float _scaleSize = 0.2f;

    [Header("Linking")]
    [SerializeField] RectTransform _origin;
    [SerializeField] RectTransform _line;

    Vector2 _originPos;

    void Start()
    {
        SetActive(false);
    }

    public void SetActive(bool state)
    {
        _origin.gameObject.SetActive(state);
        _line.gameObject.SetActive(state);
    }

    void SetPos(Vector2 pos)
    {
        _origin.position = pos;
        _line.position = pos;
    }

    public void SetOrigin(Vector2 screenPosition)
    {
        _originPos = screenPosition;

        SetActive(true);
        SetPos(screenPosition);
    }

    public void SetScale(Vector2 currentPos)
    {
        Vector2 offset = currentPos - _originPos;

        float orientation = transform.eulerAngles.z;
        float angle = Vector2.SignedAngle(Vector2.up, offset);
        _line.localRotation = Quaternion.Euler(0, 0, angle);

        float distance = offset.magnitude;
        float scale = distance * _scaleSize;

        Vector3 sizeDelta = _line.sizeDelta;
        sizeDelta.y = scale;
        _line.sizeDelta = sizeDelta;
    }
}
