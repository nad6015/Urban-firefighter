using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public float jumpHeight = 2.5f;
    public float lerpTime = 0.1f;
    public float gravityMultipler = 1.0f;
    public GameObject equipSlot;

    internal UrbanFireFighterActions actions;

    internal PlayerMovement movement;
    internal PlayerInteractions interaction;
    internal PlayerInventory inventory;
    internal Action<float> onDamage;

    internal Transform model;
    internal Animator animator;

    private void Awake()
    {
        actions = new UrbanFireFighterActions();

        model = transform.Find("character");
        animator = model.GetComponent<Animator>();

        movement = GetComponent<PlayerMovement>();
        interaction = GetComponent<PlayerInteractions>();
        inventory = GetComponent<PlayerInventory>();
    }

    private void OnEnable()
    {
        movement.EnableMovement(actions);
        interaction.EnableInteraction(actions);
        inventory.EnableInventory(actions);
    }

    private void OnDisable()
    {
        movement.DisableMovement(actions);
        interaction.DisableInteraction(actions);
        inventory.DisableInventory(actions);
    }

    // Controller collision code referenced from - https://www.reddit.com/r/Unity3D/comments/147ymoh/basic_question_regarding_character_controller/
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject gameObject = hit.gameObject;
        if (gameObject.layer == 9)
        {
            movement.ApplyForce();
            onDamage(gameObject.GetComponent<Fire>().FireStr);
        }
        if (gameObject.layer == 8)
        {
            gameObject.GetComponent<Interactable>().Interact(gameObject);
        }
    }
}
