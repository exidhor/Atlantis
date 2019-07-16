using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] PlayerShip _ship;

    Vector3 _offset;

    void OnEnable()
    {
        _offset = _ship.transform.position - transform.position;
    }

    public void Follow()
    {
        transform.position = _ship.transform.position - _offset;
    }
}
