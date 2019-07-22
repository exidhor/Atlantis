using UnityEngine;
using System.Collections;

public class FishingFloat : MonoBehaviour
{
    public Vector3 floatPosition
    {
        get { return _model.position; }
    }

    [SerializeField] float _appearTime;
    [SerializeField] float _disappearTime;
    [SerializeField] Transform _model;

    bool _isAppearing;
    bool _isDisappearing;

    float _time;

    public void Appear()
    {
        _isAppearing = true;
        _isDisappearing = false;
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);

        _time = 0f;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetPosition2D(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    public void Disappear()
    {
        _isAppearing = false;
        _isDisappearing = true;
        transform.localScale = Vector3.one;

        _time = 0f;
    }

    void LateUpdate()
    {
        _time += Time.deltaTime;

        if (_isAppearing)
        {
            if(_time > _appearTime)
            {
                _time = _appearTime;
                _isAppearing = false;
            }

            float scale = _time / _appearTime;

            transform.localScale = Vector3.one * scale;
        }

        else if (_isDisappearing)
        {
            if (_time > _disappearTime)
            {
                _time = _disappearTime;
                _isDisappearing = false;
                gameObject.SetActive(false);
            }

            float scale = 1 - _time / _disappearTime;

            transform.localScale = Vector3.one * scale;
        }
    }
}
