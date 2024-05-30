using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public float jumpHeight = 2.5f;
    public float lerpTime = 0.1f;
    public float gravityMultipler = 1.0f;

    private MovementHandler _handler;
    private RhythmBreakerActions _actions;

    private void Awake()
    {
        _actions = new RhythmBreakerActions();
        _handler = GetComponent<MovementHandler>();
        EnableMovement();
    }

    private void OnEnable()
    {
        EnableMovement();
    }

    private void OnDisable()
    {
        DisableMovement();
    }

    public void DisableMovement()
    {
        _actions.Default.Movement.Disable();
        _actions.Default.Interact.Disable();

        _actions.Default.Movement.canceled -= _handler.OnCanceled;
        _actions.Default.Movement.performed -= _handler.OnMovement;

        _actions.Default.Interact.performed -= _handler.OnInteract;
    }

    public void EnableMovement()
    {
        _actions.Default.Movement.Enable();
        _actions.Default.Interact.Enable();

        _actions.Default.Movement.canceled += _handler.OnCanceled;
        _actions.Default.Movement.performed += _handler.OnMovement;

        _actions.Default.Interact.performed += _handler.OnInteract;
    }
}
