using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private static LevelManager manager;
    // Code to serialize private field referenced from - https://discussions.unity.com/t/make-a-public-variable-with-a-private-setter-appear-in-inspector/132173
    [SerializeField]
    float maxFireStr = 7f;
    public float FireStr { get { return fireStr; } }

    private float fireStr = 0;

    private void Start()
    {
        fireStr = maxFireStr;
        if (manager == null)
        {
            manager = FindObjectOfType<LevelManager>();
        }
    }

    internal void Extinguish(float waterStr)
    {
        fireStr -= waterStr;

        foreach (var ps in GetComponentsInChildren<ParticleSystem>())
        {
            ps.transform.localScale = Vector3.Slerp(ps.transform.localScale, ps.transform.localScale * Mathf.Max(FireStr / maxFireStr, 0.9f), 0.1f);
        }

        if (FireStr <= 0)
        {
            manager.DecreaseFireCount();
            manager.IsLevelCompleted();
            FindObjectOfType<ObjectivesController>().ExtinguishFire();
            gameObject.SetActive(false);
        }
    }
}
