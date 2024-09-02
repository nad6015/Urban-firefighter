using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    private Interactable interactable;

    internal void EnableInteraction(UrbanFireFighterActions actions)
    {
        actions.Default.Interact.Enable();
        actions.Default.Interact.performed += OnInteract;
    }

    internal void DisableInteraction(UrbanFireFighterActions actions)
    {
        actions.Default.Interact.Disable();
        actions.Default.Interact.performed -= OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (interactable != null)
        {
            interactable.Interact(gameObject);
            interactable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        interactable = other.GetComponent<Interactable>();
    }

    private void OnTriggerExit(Collider other)
    {
        interactable = null;
    }
}
