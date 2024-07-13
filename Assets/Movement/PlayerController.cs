using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public float jumpHeight = 2.5f;
    public float lerpTime = 0.1f;
    public float gravityMultipler = 1.0f;
    public ParticleSystem spray;

    public bool HasStopped { get; set; }
    protected Vector3 _motion;
    protected Vector2 _motionZero = Vector3.zero;

    private RhythmBreakerActions _actions;
    private Rigidbody2D _body2D;
    private Interactable _interactable;
    private bool _isSpraying;
    private FireExtinguisher hose;
    private float timeToDecreaseWater = 0;
    private void Awake()
    {
        _actions = new RhythmBreakerActions();
    }
    private void Start()
    {
        _body2D = GetComponent<Rigidbody2D>();
        hose = spray.GetComponent<FireExtinguisher>();
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
        _actions.Default.Spray.Disable();
        _actions.Default.Jump.Disable();

        _actions.Default.Movement.canceled -= OnMovementCanceled;
        _actions.Default.Movement.performed -= OnMovement;

        _actions.Default.Interact.performed -= OnInteract;

        _actions.Default.Spray.canceled -= OnSprayCanceled;
        _actions.Default.Spray.performed -= OnSpray;

        _actions.Default.Jump.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
       _motion.y = jumpHeight;
        HasStopped = false;
    }

    public void EnableMovement()
    {
        _actions.Default.Movement.Enable();
        _actions.Default.Interact.Enable();
        _actions.Default.Spray.Enable();
        _actions.Default.Jump.Enable();

        _actions.Default.Movement.canceled += OnMovementCanceled;
        _actions.Default.Movement.performed += OnMovement;

        _actions.Default.Interact.performed += OnInteract;

        _actions.Default.Spray.canceled += OnSprayCanceled;
        _actions.Default.Spray.performed += OnSpray;

        _actions.Default.Jump.performed += OnJumpCanceled;
        _actions.Default.Jump.performed += OnJump;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        HasStopped = true;
        _motion.y = 0;
    }

    private void Update()
    {
        timeToDecreaseWater -= Time.deltaTime;

        if (HasStopped)
        {
            _motion = Vector3.Slerp(_motion, _motionZero, lerpTime);
        }

        _body2D.AddForce(_motion);

        if (_isSpraying)
        {
            // Directional calculation code referenced from - https://discussions.unity.com/t/rotate-object-weapon-towards-mouse-cursor-2d/1172/4
            Vector3 mousePosition = Input.mousePosition;

            Vector3 sprayScreenPos = Camera.main.WorldToScreenPoint(spray.transform.position);

            mousePosition.x = mousePosition.x - sprayScreenPos.x;
            mousePosition.y = mousePosition.y - sprayScreenPos.y;

            float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

            spray.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if (timeToDecreaseWater <= 0)
            {
                hose.decreaseWaterSupply();
                timeToDecreaseWater = 1f;
            }
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (_interactable != null)
        {
            _interactable.Interact(gameObject);
        }
    }

    private void OnSpray(InputAction.CallbackContext context)
    {
        // Hold action code referenced from - https://forum.unity.com/threads/wait-theres-seriously-not-a-held-button-interaction-in-the-new-input-system.1417059/
        _isSpraying = context.action.IsPressed();
        if (_isSpraying)
        {
            spray.Play();
        }
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        HasStopped = false;

        var _movement = context.ReadValue<Vector2>();

        _motion = _movement;

        _motion.Normalize();
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        HasStopped = true;
        _motion = Vector3.zero;
    }

    private void OnSprayCanceled(InputAction.CallbackContext context)
    {
        _isSpraying = false;
        spray.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _interactable = other.GetComponent<Interactable>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _interactable = null;
    }
}
