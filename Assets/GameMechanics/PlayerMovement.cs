using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

// TODO: Animation code can be refactored into another class
public class PlayerMovement : MonoBehaviour
{   
    private Vector3 _dir;
    private Vector3 _motionZero = Vector3.zero;
    private Vector3 _inputMotion;
    private Vector3 _outsideForce;

    private float pushbackMag = 10f;

    private bool _ignoreInput;
    private bool _hasStopped = false;

    private Transform _root;
    private Animator _animator;

    protected PlayerController _pc;
    protected CharacterController _controller;

    private void Start()
    {
        _pc = GetComponent<PlayerController>();
        _controller = GetComponent<CharacterController>();
        Transform model = transform.Find("character");
        _animator = model.GetComponent<Animator>();
        _root = model;
    }

    internal void EnableMovement(UrbanFireFighterActions actions)
    {
        actions.Default.Movement.Enable();
        actions.Default.Spray.Enable();
        actions.Default.Jump.Enable();

        actions.Default.Movement.canceled += OnMovementCanceled;
        actions.Default.Movement.performed += OnMovement;

        actions.Default.Jump.performed += OnJump;
    }

    internal void DisableMovement(UrbanFireFighterActions actions)
    {
        actions.Default.Movement.Disable();
        actions.Default.Spray.Disable();
        actions.Default.Jump.Disable();

        actions.Default.Movement.canceled -= OnMovementCanceled;
        actions.Default.Movement.performed -= OnMovement;
        actions.Default.Jump.performed -= OnJump;
    }

    private void Update()
    {
        Vector3 motion = Vector3.zero;
        if (_ignoreInput)
        {
            CalculateCharacterY(ref _outsideForce, 0f);
            CalculateMotion(ref _outsideForce);
            motion = _outsideForce;
        }
        else
        {
            CalculateCharacterY(ref _inputMotion, -1f);
            CalculateMotion(ref _inputMotion);
            motion = _inputMotion;
        }

        _controller.Move(motion * Time.deltaTime * _pc.speed);
        if (_dir != Vector3.zero)
        {
            _root.transform.rotation = Quaternion.Slerp(_root.transform.rotation, Quaternion.LookRotation(_dir, Vector3.up), _pc.lerpTime);
        }

    }

    private void CalculateCharacterY(ref Vector3 _motion, float gravityBoundary)
    {
        if (_controller.isGrounded)
        {
            _motion.y = Mathf.Clamp(_motion.y, gravityBoundary, _pc.jumpHeight);
            _animator.SetBool("isJumping", false);
        }
        else
        {
            _motion.y += Time.deltaTime * Physics.gravity.y * _pc.gravityMultipler;
            _animator.SetBool("isJumping", true);
        }
    }

    private void CalculateMotion(ref Vector3 _motion)
    {
        // If there's no input from the player or an external force is acting on the player
        if (_hasStopped || _ignoreInput)
        {
            _motionZero.y = _motion.y;
            _motion = Vector3.Slerp(_motion, _motionZero, _pc.lerpTime);

            // If the external force has been expended
            if (_ignoreInput && Vector3.Distance(_motion, _motionZero) < 0.1f)
            {
                _ignoreInput = false;
            }
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_controller.isGrounded)
        {
            _inputMotion.y = _pc.jumpHeight;
            _animator.SetBool("isJumping", true);
        }
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        _hasStopped = false;

        var _movement = context.ReadValue<Vector2>();

        _inputMotion.Set(_movement.x, _inputMotion.y, _movement.y);
        if (_movement != Vector2.zero)
        {
            _dir.Set(_inputMotion.x, 0, _inputMotion.z);
        }
        _dir.Set(_movement.x, 0, _movement.y);

        _inputMotion.Normalize();
        _dir.Normalize();

        // TODO: Might be able to move animation code into separate class
        _animator.SetBool("isMoving", true);
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        _hasStopped = true;
        _inputMotion = Vector3.zero;
        _animator.SetBool("isMoving", false);
    }

    internal void ApplyForce()
    {
        _outsideForce = -_inputMotion * pushbackMag;
        _outsideForce.y = 0f;

        _ignoreInput = true;
    }

    internal Vector3 GetModelForward()
    {
        return _dir;
    }
}
