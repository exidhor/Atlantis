﻿using UnityEngine;
using System.Collections;
using Tools;

public class PlayerControls : MonoSingleton<PlayerControls>
{
    public bool shipInput
    {
        get { return _shipInput; }
    }

    [Header("Move")]
    [SerializeField] float _moveScale = 0.5f;
    [SerializeField] float _maxDistance = 3f;

    [Header("Linking")]
    [SerializeField] PlayerInputFeedback _feedback;
    [SerializeField] PlayerCamera _camera;
    [SerializeField] HarborHandler _harborHandler;

    bool _shipInput;
    Vector2 _originScreenPoint;

    Vector2 _move;

    void Update()
    {
        if (!MainManager.instance.started) return;

        HandleMovement();
        HandleDeal();
    }

    void HandleMovement()
    {
        if (!_shipInput
            && Input.GetMouseButtonDown(0)
            && !UILayoutInventory.instance.playerOnShipHold)
        {
            _shipInput = true;
            _originScreenPoint = Input.mousePosition;
            _feedback.SetOrigin(_originScreenPoint);
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            _move = mousePosition - _originScreenPoint;

            if (_move.magnitude > _maxDistance)
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

        _camera.Follow(Time.deltaTime);
    }

    void HandleDeal()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _harborHandler.AskForDeal();
        }
    }

    void FixedUpdate()
    {
        if(_shipInput)
        {
            PlayerShip.instance.Move(false, _move * _moveScale, Time.fixedDeltaTime);
        }
        else
        {
            PlayerShip.instance.Move(true, Vector2.zero, Time.fixedDeltaTime);
        }
    }
}
