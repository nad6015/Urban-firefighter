using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Code to serialize private field referenced from - https://discussions.unity.com/t/make-a-public-variable-with-a-private-setter-appear-in-inspector/132173
    [SerializeField]
    float maxFireStr = 10f;
    public float FireStr { get { return fireStr; } }

    private float fireStr = 10f;

    private void Start()
    {
        fireStr = maxFireStr;
    }

    internal void Extinguish(float waterStr)
    {
        fireStr -= waterStr;

        foreach (var ps in GetComponentsInChildren<ParticleSystem>())
        {
            ps.transform.localScale = Vector3.Slerp(ps.transform.localScale, ps.transform.localScale * Mathf.Max((FireStr / maxFireStr), 0f), 0.1f);
        }

        if (FireStr <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
