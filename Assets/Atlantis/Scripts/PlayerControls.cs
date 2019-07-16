using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float _moveScale = 0.5f;
    [SerializeField] float _maxDistance = 3f;

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
            _feedback.SetOrigin(_originScreenPoint);
        }

        if(Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            _move = mousePosition - _originScreenPoint;

            if(_move.magnitude > _maxDistance)
            {
                _move.Normalize();
                _move *= _maxDistance;
            }

            _feedback.SetScale(_move);
        }
        else
        {
            _shipInput = false;
            _feedback.SetActive(false);
        }

        _camera.Follow();
    }

    void FixedUpdate()
    {
        if(_shipInput)
        {
            _ship.Move(false, _move * _moveScale, Time.fixedDeltaTime);
        }
        else
        {
            _ship.Move(true, Vector2.zero, Time.fixedDeltaTime);
        }
    }
}
