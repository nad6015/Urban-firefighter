using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    private UrbanFireFighterActions _actions;
    private Interactable _interactable;

    private void OnStart()
    {
        _actions = GetComponent<PlayerController>()._actions;
    }


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
        if (_interactable != null)
        {
            Debug.Log("Interactable was found!");
            _interactable.Interact(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _interactable = other.GetComponent<Interactable>();
    }

    private void OnTriggerExit(Collider other)
    {
        _interactable = null;
    }

}
