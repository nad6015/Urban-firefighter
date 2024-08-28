using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Breakable : MonoBehaviour
{
    public float toughness = 9;

    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    internal void TakeDamage(float axeStr)
    {
        toughness -= axeStr;
        _animator.Play("onHit");
        _animator.SetFloat("toughness", toughness);

        if (toughness < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
