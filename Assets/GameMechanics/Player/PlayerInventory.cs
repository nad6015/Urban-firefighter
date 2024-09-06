using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;

public class PlayerInventory : MonoBehaviour
{
    private PlayerTool currentlyEquipped;
    public List<PlayerTool> equipment;
    private GameObject equipSlot;
    private PlayerController playerController;
    private int activeSlot = -1;

    void Start()
    {
        equipment = new List<PlayerTool>();
        playerController = GetComponent<PlayerController>();
        equipSlot = playerController.equipSlot;
    }

    internal void PickUp(GameObject gameObj)
    {
        gameObj.TryGetComponent(out PlayerTool pickupRef);
        if (pickupRef != null)
        {
            pickupRef.transform.SetParent(equipSlot.transform);
            Vector3 pickupPos = equipSlot.transform.position;
            pickupPos.y -= 1f;
            pickupRef.transform.SetPositionAndRotation(pickupPos, Quaternion.LookRotation(playerController.movement.GetModelForward(), playerController.transform.up));

            if (currentlyEquipped != null)
            {
                currentlyEquipped.gameObject.SetActive(false);
            }

            currentlyEquipped = pickupRef;
            equipment.Add(pickupRef);
            activeSlot = equipment.Count - 1;
        }
    }

    internal void EnableInventory(UrbanFireFighterActions actions)
    {
        actions.Default.Use.Enable();
        actions.Default.Use.performed += OnClick;
        actions.Default.Use.canceled += OnRelease;

        actions.Default.SwitchItem.Enable();
        actions.Default.SwitchItem.performed += OnSwitch;
    }

    internal void DisableInventory(UrbanFireFighterActions actions)
    {
        actions.Default.Use.Disable();
        actions.Default.Use.performed -= OnClick;

        actions.Default.SwitchItem.Disable();
        actions.Default.SwitchItem.performed -= OnSwitch;
    }

    private void OnSwitch(InputAction.CallbackContext context)
    {
        activeSlot++;
        if (activeSlot >= equipment.Count)
        {
            activeSlot = 0;
        }

        if (currentlyEquipped != null)
        {
            currentlyEquipped.gameObject.SetActive(false);
            equipment[activeSlot].gameObject.SetActive(true);
        }

        currentlyEquipped = equipment[activeSlot];
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (currentlyEquipped != null)
        {
            currentlyEquipped.Use();
        }
    }
    private void OnRelease(InputAction.CallbackContext context)
    {
        if (currentlyEquipped != null)
        {
            currentlyEquipped.StopUsing();
        }
    }

    // C# Generic code referenced from  - https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-methods
    // Generic constraints code referenced from - https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters?source=recommendations
    internal T GetItem<T>(string v) where T : PlayerTool
    {
        return (T)equipment.Find(x => x is T);
    }
}
