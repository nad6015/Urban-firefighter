using System;
using UnityEngine;

public class FireExtinguisher : PlayerTool
{
    public float maxExtinguisherLevel = 100f;
    private float currentExtinguisherLevel;

    public float extinguisherUsageRate = 5f; // How much extinguisher is used per second
    public float refillRate = 20f; // How much extinguisher is refilled per second

    internal Action<float> onUse; // Event triggered when extinguiser is used/spraying
    internal Action<float> onRefill; // Event triggered when extinguiser is being refilled
    private bool isSpraying;
    private ParticleSystem foam; // reference to particle system that simulates extinguisher's spray

    private PlayerController pc;

    void Start()
    {
        currentExtinguisherLevel = maxExtinguisherLevel;
        foam = GetComponentInChildren<ParticleSystem>();
        foam.Stop();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isSpraying)
        {
            UseExtinguisher(extinguisherUsageRate * Time.deltaTime);
            ExtinguishFire();

            if(currentExtinguisherLevel <= 0)
            {
                StopUsing();
            }
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

    public override void Use()
    {
        if (currentExtinguisherLevel > 0)
        {
            isSpraying = true;
            foam.Play();
            useSFX.Play();
        }
    }

    public override void StopUsing()
    {
        isSpraying = false;
        foam.Stop();
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
        Vector3 playerForward = pc.transform.position + (pc.movement.GetModelForward() * 3f);
        if (Physics.Linecast(pc.transform.position, playerForward, out RaycastHit hit) && hit.collider.TryGetComponent(out Fire fire))
        {
            fire.Extinguish(Time.deltaTime * extinguisherUsageRate);
        }
    }

    internal bool IsFull()
    {
        return currentExtinguisherLevel == maxExtinguisherLevel;
    }
}
