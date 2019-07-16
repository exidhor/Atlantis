using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
    [Header("Linking")]
    [SerializeField] PlayerInputFeedback _feedback;
    [SerializeField] PlayerShip _ship;
    [SerializeField] PlayerCamera _camera;

    bool _shipInput;
    Vector2 _originScreenPoint;

    Vector2 _move;

    void Update()
    {
        if(!_shipInput
            && Input.GetMouseButtonDown(0))
        {
            _shipInput = true;
            _originScreenPoint = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            _move = _originScreenPoint - (Vector2)Input.mousePosition;
        }
        else
        {
            _shipInput = false;
        }

        _camera.Follow();
    }

    void FixedUpdate()
    {
        if(_shipInput)
        {
            _ship.Move(false, _move, Time.fixedDeltaTime);
        }
        else
        {
            _ship.Move(true, Vector2.zero, Time.fixedDeltaTime);
        }
    }
}
