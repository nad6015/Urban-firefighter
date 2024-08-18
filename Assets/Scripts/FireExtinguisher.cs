using System;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public float maxExtinguisherLevel = 100f;
    private float currentExtinguisherLevel;

    public float extinguisherUsageRate = 5f; // How much extinguisher is used per second
    public float refillRate = 20f; // How much extinguisher is refilled per second

    public Transform firePoint; // Point from where the extinguisher will spray
    internal Action<float> onUse; // Event triggered when extinguiser is used/spraying
    internal Action<float> onRefill; // Event triggered when extinguiser is being refilled
    private bool _isSpraying;
    private ParticleSystem _foam; // reference to particle system that simulates extinguisher's spray
    private float timeToApplyWater = 0; // Time until spray is applied to fire again

    void Start()
    {
        currentExtinguisherLevel = maxExtinguisherLevel;
        _foam = GetComponentInChildren<ParticleSystem>();
        _foam.Stop();
    }

    void Update()
    {
        if (_isSpraying)
        {
            UseExtinguisher(extinguisherUsageRate * Time.deltaTime);
            ExtinguishFire();
        }

        // Example refill of the fire extinguisher with a key press (right mouse button)
        if (Input.GetMouseButton(1))
        {
            RefillExtinguisher(Time.deltaTime * refillRate);
        }
    }

    private void UseExtinguisher(float amount)
    {
        currentExtinguisherLevel -= amount;
        if (currentExtinguisherLevel < 0)
        {
            currentExtinguisherLevel = 0;
        }
        onUse(currentExtinguisherLevel);
    }

    public void StartSpraying()
    {
        _isSpraying = true;
        _foam.Play();
    }

    public void StopSpraying()
    {
        _isSpraying = false;
        _foam.Stop();
    }

    public void RefillExtinguisher(float amount)
    {
        currentExtinguisherLevel += amount;
        if (currentExtinguisherLevel > maxExtinguisherLevel)
        {
            currentExtinguisherLevel = maxExtinguisherLevel;
        }
        onRefill(amount);
    }

    private void ExtinguishFire()
    {

        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 3f))
        {
            Fire fire = hit.collider.GetComponent<Fire>();
            fire?.Extinguish(Time.deltaTime * extinguisherUsageRate);
        }
    }
}
