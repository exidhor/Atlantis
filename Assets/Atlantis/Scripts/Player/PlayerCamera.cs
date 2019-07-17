using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float _moveOffsetScale;
    [SerializeField] float _speedOffset;
    [SerializeField] float speedScale;
    [SerializeField] PlayerShip _ship;

    Vector3 _offsetOrigin;
    Vector3 _offset;

    void OnEnable()
    {
        _offsetOrigin = _ship.transform.position - transform.position;
    }

    public void Follow(float dt)
    {
        Vector3 pos = _ship.transform.position - _offsetOrigin;

        Vector3 target = _ship.velocity * _moveOffsetScale;

        float maxDist = _speedOffset * dt;
        float t = Mathf.Lerp(0f, 1f, (target - _offset).magnitude * speedScale);
        Vector3 o = Vector3.MoveTowards(_offset, target, maxDist * t);

        transform.position = pos + o;

        _offset = o;
    }
}
