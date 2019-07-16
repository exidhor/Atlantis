using UnityEngine;
using System.Collections;

public class PlayerInputFeedback : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] float _scaleSize = 0.2f;

    [Header("Linking")]
    [SerializeField] GameObject _origin;
    [SerializeField] GameObject _line;

    Vector2 _originPos;

    void Start()
    {
        SetActive(false);
    }

    public void SetActive(bool state)
    {
        _origin.SetActive(state);
        _line.SetActive(state);
    }

    void SetPos(Vector2 pos)
    {
        _origin.transform.position = pos;
        _line.transform.position = pos;
    }

    public void SetOrigin(Vector2 screenPosition)
    {
        _originPos = screenPosition;

        SetActive(true);
        SetPos(screenPosition);
    }

    public void SetScale(Vector2 currentPos)
    {
        _line.transform.LookAt(currentPos);
        Vector2 offset = currentPos - _originPos;
        float distance = offset.magnitude;
        float scale = distance * _scaleSize;

        Vector3 vScale = transform.localScale;
        vScale.y = scale;
        transform.localScale = vScale;
    }
}
