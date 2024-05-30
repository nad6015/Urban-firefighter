using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public abstract class MovementHandler : MonoBehaviour
{
    public bool HasStopped { get; set; }
    protected Vector3 _motion;
    protected Vector3 _motionZero = Vector3.zero;
    protected CharacterController _controller;
    protected PlayerController _playerController;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerController = GetComponent<PlayerController>();
    }

    internal abstract void OnCanceled(InputAction.CallbackContext context);
    internal abstract void OnInteract(InputAction.CallbackContext context);
    internal abstract void OnMovement(InputAction.CallbackContext context);
}