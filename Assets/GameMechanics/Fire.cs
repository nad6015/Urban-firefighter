using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    float fireStr = 10f;
    float maxFireStr = 10f;
    private float timeToApplyWater = 0;
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
            transform.localScale = Vector3.one * Mathf.Max((fireStr / maxFireStr), 0.3f);
        }

        if (fireStr == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
