using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

// TODO: Animation code can be refactored into another class
public class PlayerMovement : MonoBehaviour
{
    private Vector3 dir;
    private Vector3 motionZero = Vector3.zero;
    private Vector3 inputMotion;
    private Vector3 outsideForce;

    private float pushbackMag = 10f;

    private bool ignoreInput;
    private bool hasStopped = false;

    private Transform root;
    private Animator animator;

    protected PlayerController pc;
    protected CharacterController controller;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        controller = GetComponent<CharacterController>();

        root = pc.model;
        animator = pc.animator;
    }

    internal void EnableMovement(UrbanFireFighterActions actions)
    {
        actions.Default.Movement.Enable();
        actions.Default.Jump.Enable();

        actions.Default.Movement.canceled += OnMovementCanceled;
        actions.Default.Movement.performed += OnMovement;

        actions.Default.Jump.performed += OnJump;
    }

    internal void DisableMovement(UrbanFireFighterActions actions)
    {
        actions.Default.Movement.Disable();
        actions.Default.Jump.Disable();

        actions.Default.Movement.canceled -= OnMovementCanceled;
        actions.Default.Movement.performed -= OnMovement;
        actions.Default.Jump.performed -= OnJump;
    }

    private void Update()
    {
        Vector3 motion;
        if (ignoreInput)
        {
            CalculateCharacterY(ref outsideForce, 0f);
            CalculateMotion(ref outsideForce);
            motion = outsideForce;
        }
        else
        {
            CalculateCharacterY(ref inputMotion, -1f);
            CalculateMotion(ref inputMotion);
            motion = inputMotion;
        }

        controller.Move(Time.deltaTime * pc.speed * motion);
        if (dir != Vector3.zero)
        {
            root.transform.rotation = Quaternion.Slerp(root.transform.rotation, Quaternion.LookRotation(dir, Vector3.up), pc.lerpTime);
        }

    }

    private void CalculateCharacterY(ref Vector3 _motion, float gravityBoundary)
    {
        if (controller.isGrounded)
        {
            _motion.y = Mathf.Clamp(_motion.y, gravityBoundary, pc.jumpHeight);
            animator.SetBool("isJumping", false);
        }
        else
        {
            _motion.y += Time.deltaTime * Physics.gravity.y * pc.gravityMultipler;
            animator.SetBool("isJumping", true);
        }
    }

    private void CalculateMotion(ref Vector3 _motion)
    {
        // If there's no input from the player or an external force is acting on the player
        if (hasStopped || ignoreInput)
        {
            motionZero.y = _motion.y;
            _motion = Vector3.Slerp(_motion, motionZero, pc.lerpTime);

            // If the external force has been expended
            if (ignoreInput && Vector3.Distance(_motion, motionZero) < 0.1f)
            {
                ignoreInput = false;
            }
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (controller.isGrounded)
        {
            inputMotion.y = pc.jumpHeight;
            animator.SetBool("isJumping", true);
        }
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        hasStopped = false;

        var _movement = context.ReadValue<Vector2>();

        inputMotion.Set(_movement.x, inputMotion.y, _movement.y);
        if (_movement != Vector2.zero)
        {
            dir.Set(inputMotion.x, 0, inputMotion.z);
        }
        dir.Set(_movement.x, 0, _movement.y);

        inputMotion.Normalize();
        dir.Normalize();

        // TODO: Might be able to move animation code into separate class
        animator.SetBool("isMoving", true);
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        hasStopped = true;
        inputMotion = Vector3.zero;
        animator.SetBool("isMoving", false);
    }

    internal void ApplyForce()
    {
        outsideForce = -inputMotion * pushbackMag;
        outsideForce.y = 0f;

        ignoreInput = true;
    }

    internal Vector3 GetModelForward()
    {
        return dir;
    }
}
