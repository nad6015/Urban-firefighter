using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public float jumpHeight = 2.5f;
    public float lerpTime = 0.1f;
    public float gravityMultipler = 1.0f;
    public bool isMoving = false;

    private bool HasStopped { get; set; }
    private Vector3 _motion;
    private Vector3 _dir;
    private Vector3 _motionZero = Vector3.zero;

    private UrbanFireFighterActions _actions;
    private Interactable _interactable;
    protected CharacterController _controller;

    private Transform _root;
    private Animator _animator;

    private bool hasFireExtinguisher = false;

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

    private void OnSpray(InputAction.CallbackContext context)
    {
        if (hasFireExtinguisher)
        {
            DischargeFireExtinguisher();
        }
    }

    private void DischargeFireExtinguisher()
    {
        // Logic for discharging the fire extinguisher
        Debug.Log("Discharging fire extinguisher!");

        // Interaction with fire objects
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
        {
            Fire fire = hit.collider.GetComponent<Fire>();
            if (fire != null)
            {
                // fire.Extinguish(10f); // Extinguish the fire method. But I can see there is some other intention in the Fire object, there is OnParticleCollision method
                hasFireExtinguisher = false;
            }
        }
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
        _actions.Default.Spray.performed += OnSpray;

        _actions.Default.Jump.performed += OnJumpCanceled;
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
        CalculateCharacterY();

        if (HasStopped)
        {
            _motionZero.y = _motion.y;
            _motion = Vector3.Slerp(_motion, _motionZero, lerpTime);
        }

        _controller.Move(_motion * Time.deltaTime * speed);
        if (_dir != Vector3.zero)
        {
            _root.transform.rotation = Quaternion.Slerp(_root.transform.rotation, Quaternion.LookRotation(_dir, Vector3.up), lerpTime);
        }

    }

    private void CalculateCharacterY()
    {
        if (_controller.isGrounded)
        {
            _motion.y = Mathf.Clamp(_motion.y, -1f, jumpHeight);
            _animator.SetBool("isJumping", false);
        }
        else
        {
            _motion.y += Time.deltaTime * Physics.gravity.y * gravityMultipler;
            _animator.SetBool("isJumping", true);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_controller.isGrounded)
        {
            _motion.y = jumpHeight;
            _animator.SetBool("isJumping", true);
        }
    }


    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
      
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (_interactable != null)
        {
            if (_interactable.CompareTag("FireExtinguisher"))
            {
                hasFireExtinguisher = true;
                Destroy(_interactable.gameObject); // Pick up and destroy the extinguisher in the scene
                Debug.Log("Picked up fire extinguisher!");
            }
            else
            {
                _interactable.Interact(gameObject);
            }
        }
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        HasStopped = false;

        var _movement = context.ReadValue<Vector2>();

        _motion.Set(_movement.x, _motion.y, _movement.y);
        if (_movement != Vector2.zero)
        {
            _dir.Set(_motion.x, 0, _motion.z);
        }
        _dir.Set(_movement.x,0, _movement.y);

        _motion.Normalize();
        _dir.Normalize();

        // TODO: Might be able to move animation code into separate class
        _animator.SetBool("isMoving", true);
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        HasStopped = true;
        _motion = Vector3.zero;
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
}
