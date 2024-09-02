using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactable
{
    public GameObject _pickup;

    private Animator _animator;
    private SpriteRenderer _prompt;
    private GameObject pickupRef;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _prompt = GetComponentInChildren<SpriteRenderer>();
        pickupRef = Instantiate(_pickup, transform.GetChild(0));
    }

    private void OnTriggerEnter(Collider other)
    {
        _animator.enabled = true;
        _prompt.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _animator.enabled = false;
        _prompt.enabled = false;
    }

    public override void Interact(GameObject player)
    {
        if (pickupRef.TryGetComponent(out FireExtinguisher extinguisher) && GameObject.FindGameObjectWithTag("UI_Controller").TryGetComponent(out FireExtinguisherGauge gauge))
        {
            gauge.SetMaxLevel(extinguisher.maxExtinguisherLevel);

            extinguisher.onUse += gauge.SetLevel;
            extinguisher.onRefill += gauge.SetLevel;
        }

        player.GetComponent<PlayerInventory>().PickUp(pickupRef);
        gameObject.SetActive(false);
    }
}
