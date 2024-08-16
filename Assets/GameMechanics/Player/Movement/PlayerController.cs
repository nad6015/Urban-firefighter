using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public float jumpHeight = 2.5f;
    public float lerpTime = 0.1f;
    public float gravityMultipler = 1.0f;
    private float pushbackMag = 10f;

    private bool _hasStopped { get; set; }
    private Vector3 _inputMotion;
    private Vector3 _outsideForce;
    private bool _ignoreInput;
    private Vector3 _dir;
    private Vector3 _motionZero = Vector3.zero;

    private UrbanFireFighterActions _actions;
    private Interactable _interactable;
    protected CharacterController _controller;

    private Transform _root;
    private Animator _animator;
    internal Action<float> onDamage;

    private void Awake()
    {
        _actions = new UrbanFireFighterActions();
        _controller = GetComponent<CharacterController>();

        Transform model = transform.Find("character");
        _root = model;
        _animator = model.GetComponent<Animator>();
    }
    private void Start()
    {
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

    private void EnableMovement()
    {
        _actions.Default.Movement.Enable();
        _actions.Default.Interact.Enable();
        _actions.Default.Spray.Enable();
        _actions.Default.Jump.Enable();

        _actions.Default.Movement.canceled += OnMovementCanceled;
        _actions.Default.Movement.performed += OnMovement;

        _actions.Default.Interact.performed += OnInteract;

        _actions.Default.Jump.performed += OnJump;
    }

    private void DisableMovement()
    {
        _actions.Default.Movement.Disable();
        _actions.Default.Interact.Disable();
        _actions.Default.Spray.Disable();
        _actions.Default.Jump.Disable();

        _actions.Default.Movement.canceled -= OnMovementCanceled;
        _actions.Default.Movement.performed -= OnMovement;

        _actions.Default.Interact.performed -= OnInteract;

        _actions.Default.Jump.performed -= OnJump;
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

        _controller.Move(motion * Time.deltaTime * speed);
        if (_dir != Vector3.zero)
        {
            _root.transform.rotation = Quaternion.Slerp(_root.transform.rotation, Quaternion.LookRotation(_dir, Vector3.up), lerpTime);
        }

    }

    private void CalculateCharacterY(ref Vector3 _motion, float gravityBoundary)
    {
        if (_controller.isGrounded)
        {
            _motion.y = Mathf.Clamp(_motion.y, gravityBoundary, jumpHeight);
            _animator.SetBool("isJumping", false);
        }
        else
        {
            _motion.y += Time.deltaTime * Physics.gravity.y * gravityMultipler;
            _animator.SetBool("isJumping", true);
        }
    }

    private void CalculateMotion(ref Vector3 _motion)
    {
        // If there's no input from the player or an external force is acting on the player
        if (_hasStopped || _ignoreInput)
        {
            _motionZero.y = _motion.y;
            _motion = Vector3.Slerp(_motion, _motionZero, lerpTime);

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
            _inputMotion.y = jumpHeight;
            _animator.SetBool("isJumping", true);
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (_interactable != null)
        {
            _interactable.Interact(gameObject);
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

    // TODO: Player interaction code can be refactored in separate class, but will need reference to InputActions
    // In this class
    private void OnTriggerEnter2D(Collider2D other)
    {
        _interactable = other.GetComponent<Interactable>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _interactable = null;
    }


    // Controller collision code referenced from - https://www.reddit.com/r/Unity3D/comments/147ymoh/basic_question_regarding_character_controller/
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject gameObject = hit.gameObject;
        if (gameObject.layer == 9)
        {
            //gameObject.GetComponent<Fire>().damagePlayer(this);
            onDamage(gameObject.GetComponent<Fire>().FireStr);
            _outsideForce = -_inputMotion * pushbackMag;
            _outsideForce.y = 0f;

            _ignoreInput = true;
        }
    }
}
