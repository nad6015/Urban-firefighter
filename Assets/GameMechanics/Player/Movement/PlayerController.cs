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
    public GameObject equipSlot;

    internal UrbanFireFighterActions _actions;

    private PlayerMovement _movement;
    private PlayerInteractions _interaction;

    internal Action<float> onDamage;
    FireExtinguisher fireExtinguisher;

    private void Awake()
    {
        _actions = new UrbanFireFighterActions();

        _movement = GetComponent<PlayerMovement>();
        _interaction = GetComponent<PlayerInteractions>();
    }

    private void OnEnable()
    {
        _movement.EnableMovement(_actions);
        _interaction.EnableInteraction(_actions);
        _actions.Default.Spray.Enable();

        _actions.Default.Spray.performed += OnClick;
        _actions.Default.Spray.canceled += OnRelease;
    }

    private void OnDisable()
    {
        _movement.DisableMovement(_actions);
        _interaction.DisableInteraction(_actions);
        _actions.Default.Spray.Disable();

        _actions.Default.Spray.performed -= OnClick;
    }

    // Controller collision code referenced from - https://www.reddit.com/r/Unity3D/comments/147ymoh/basic_question_regarding_character_controller/
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject gameObject = hit.gameObject;
        if (gameObject.layer == 9)
        {
            //gameObject.GetComponent<Fire>().damagePlayer(this);
            _movement.ApplyForce();
            onDamage(gameObject.GetComponent<Fire>().FireStr);
        }
    }

    internal void Equip(GameObject pickupRef)
    {
        // TODO: Pickup could be a different object, will need switch statement and an inventory system in future
        pickupRef.transform.SetParent(equipSlot.transform);
        Vector3 pickupPos = equipSlot.transform.position;
        pickupPos.y -= 1f;
        pickupRef.transform.SetPositionAndRotation(pickupPos, Quaternion.LookRotation(_movement.GetModelForward(), transform.up));

        fireExtinguisher = pickupRef.GetComponent<FireExtinguisher>();

        if (fireExtinguisher == null)
        {
            Debug.LogError("Fire extinguisher was not picked up.");
        }
    }
    private void OnClick(InputAction.CallbackContext context)
    {
        if (fireExtinguisher != null)
        {
            fireExtinguisher.StartSpraying();
        }
    }
    private void OnRelease(InputAction.CallbackContext context)
    {
        if (fireExtinguisher != null)
        {
            fireExtinguisher.StopSpraying();
        }
    }
}
