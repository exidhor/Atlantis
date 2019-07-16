using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
    [Header("Speed controls")]
    [SerializeField] float _acceleration;
    [SerializeField] float _startSpeed;
    [SerializeField] float _maxSpeed;

    [SerializeField] float _angularSpeed;

    [Header("Linking")]
    [SerializeField] PlayerInputFeedback _feedback;
    [SerializeField] PlayerShip _ship;
    [SerializeField] PlayerCamera _camera;

    bool _shipInputStarted;
    Vector2 _originScreenPoint;

    void Update()
    {
        if(!_shipInputStarted
            && Input.GetMouseButtonDown(0))
        {
            _shipInputStarted = true;
            _originScreenPoint = Input.mousePosition;
        }


    }
}
