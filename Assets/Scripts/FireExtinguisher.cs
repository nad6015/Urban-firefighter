using System;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public float maxExtinguisherLevel = 100f;
    private float currentExtinguisherLevel;

    public float extinguisherUsageRate = 5f; // How much extinguisher is used per second
    public float refillRate = 20f; // How much extinguisher is refilled per second
    public AudioSource useSFX;

    public Transform firePoint; // Point from where the extinguisher will spray
    internal Action<float> onUse; // Event triggered when extinguiser is used/spraying
    internal Action<float> onRefill; // Event triggered when extinguiser is being refilled
    private bool _isSpraying;
    private ParticleSystem _foam; // reference to particle system that simulates extinguisher's spray

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
        useSFX.Play();
    }

    public void StopSpraying()
    {
        _isSpraying = false;
        _foam.Stop();
        useSFX.Stop();
    }

    public void RefillExtinguisher(float amount)
    {
        currentExtinguisherLevel += amount;
        if (currentExtinguisherLevel > maxExtinguisherLevel)
        {
            currentExtinguisherLevel = maxExtinguisherLevel;
        }
        onRefill(currentExtinguisherLevel);
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

    internal bool IsFull()
    {
        return currentExtinguisherLevel == maxExtinguisherLevel;
    }
}
