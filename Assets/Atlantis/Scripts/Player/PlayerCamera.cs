using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float _moveOffsetScale;
    [SerializeField] float _speedOffset;
    [SerializeField] float _timeScale;

    Vector3 _offsetOrigin;
    Vector3 _offset;

    void OnEnable()
    {
        _offsetOrigin = PlayerShip.instance.transform.position - transform.position;
    }

    public void Follow(float dt)
    {
        Vector3 pos = PlayerShip.instance.transform.position - _offsetOrigin;

        Vector3 target = PlayerShip.instance.velocity * _moveOffsetScale;

        float maxDist = _speedOffset * dt;
        float t = Mathf.Lerp(0f, 1f, (target - _offset).magnitude * _timeScale);
        Vector3 o = Vector3.MoveTowards(_offset, target, maxDist * t);

        transform.position = pos + o;

        _offset = o;
    }
}
