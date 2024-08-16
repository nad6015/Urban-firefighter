using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Code to serialize private field referenced from - https://discussions.unity.com/t/make-a-public-variable-with-a-private-setter-appear-in-inspector/132173
    [SerializeField]
    private float fireStr = 10f;
    float maxFireStr = 10f;
    private float timeToApplyWater = 0;
    public float FireStr { get { return fireStr; } }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeToApplyWater -= Time.deltaTime;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (timeToApplyWater <= 0)
        {
            var hose = other.GetComponent<FireExtinguisher>();
            fireStr -= hose.waterStr;
            hose.DecreaseWaterSupply();

            timeToApplyWater = 1f;
            transform.localScale = Vector3.one * Mathf.Max((FireStr / maxFireStr), 0.3f);
        }

        if (FireStr == 0)
        {
            gameObject.SetActive(false);
        }
    }

    internal void damagePlayer(PlayerController playerController)
    {
        Debug.Log("HP Lost");
    }
}
