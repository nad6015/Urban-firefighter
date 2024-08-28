using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillPickup : MonoBehaviour
{
    public float refillAmount = 10.0f;
    public AudioSource refillSFX;
    bool isPickedUp = false;
    public void OnPickup(GameObject player)
    {
        FireExtinguisher fireExtinguisher = player.GetComponent<PlayerController>().inventory.GetItem<FireExtinguisher>("fire_extinguisher");
        if (fireExtinguisher != null && !fireExtinguisher.IsFull())
        {
            fireExtinguisher.RefillExtinguisher(refillAmount);
            refillSFX.Play();
            for(int i =0; i < transform.childCount; i++) { transform.GetChild(i).gameObject.SetActive(false); }
            isPickedUp = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject player = other.gameObject;
        OnPickup(player);

    }

    private void Update()
    {
        if (isPickedUp && !refillSFX.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}
