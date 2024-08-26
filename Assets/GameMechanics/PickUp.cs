using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactable
{
    public GameObject _pickup;

    private Animator _animator;
    private SpriteRenderer _prompt;
    private GameObject _pickupRef;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _prompt = GetComponentInChildren<SpriteRenderer>();
        _pickupRef = Instantiate(_pickup, transform.GetChild(0));
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
        var extinguisherGauge = GameObject.FindGameObjectWithTag("UI_Controller").GetComponent<FireExtinguisherGauge>();

        if (extinguisherGauge != null)
        {
            FireExtinguisher fireExtinguisher = _pickupRef.GetComponent<FireExtinguisher>();
            extinguisherGauge.SetMaxLevel(fireExtinguisher.maxExtinguisherLevel);

            fireExtinguisher.onUse += extinguisherGauge.SetLevel;
            fireExtinguisher.onRefill += extinguisherGauge.SetLevel;

            player.GetComponent<PlayerController>().Equip(_pickupRef);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Extinguisher Controller was not found");
        }
    }
}
