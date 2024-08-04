using UnityEngine;

public class FireExtinguisherMechanics : MonoBehaviour
{
    public float maxExtinguisherLevel = 100f;
    private float currentExtinguisherLevel;

    public float extinguisherUsageRate = 10f; // How much extinguisher is used per second
    public float refillRate = 20f; // How much extinguisher is refilled per second

    public FireExtinguisherController extinguisherController; // Reference to the UI controller
    public Transform firePoint; // Point from where the extinguisher will spray

    void Start()
    {
        currentExtinguisherLevel = maxExtinguisherLevel;
        extinguisherController.SetMaxLevel(maxExtinguisherLevel);
        extinguisherController.SetLevel(currentExtinguisherLevel);
    }

    void Update()
    {
        // Example usage of the fire extinguisher with a key press (left mouse button)
        if (Input.GetMouseButton(0) && currentExtinguisherLevel > 0)
        {
            UseExtinguisher(Time.deltaTime * extinguisherUsageRate);
            ExtinguishFire();
        }

        // Example refill of the fire extinguisher with a key press (right mouse button)
        if (Input.GetMouseButton(1))
        {
            RefillExtinguisher(Time.deltaTime * refillRate);
        }
    }

    public void UseExtinguisher(float amount)
    {
        currentExtinguisherLevel -= amount;
        if (currentExtinguisherLevel < 0)
        {
            currentExtinguisherLevel = 0;
        }
        extinguisherController.SetLevel(currentExtinguisherLevel);
    }

    public void RefillExtinguisher(float amount)
    {
        currentExtinguisherLevel += amount;
        if (currentExtinguisherLevel > maxExtinguisherLevel)
        {
            currentExtinguisherLevel = maxExtinguisherLevel;
        }
        extinguisherController.SetLevel(currentExtinguisherLevel);
    }

    private void ExtinguishFire()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit))
        {
            FireObject fire = hit.collider.GetComponent<FireObject>();
            fire?.Extinguish(Time.deltaTime * extinguisherUsageRate);
        }
    }
}
