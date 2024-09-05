using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAxe : PlayerTool
{
    private Animator player_animator;
    private PlayerController pc;

    private void Awake()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player_animator = pc.animator.GetComponent<Animator>();
    }

    public override void Use()
    {
        player_animator.Play("Axe Swing", 1);
        useSFX.Play();
        Vector3 playerForward = pc.transform.position + (pc.movement.GetModelForward() * 2f);
        Debug.DrawLine(pc.transform.position, playerForward, Color.red, 10f);
        if (Physics.Linecast(pc.transform.position, playerForward, out RaycastHit hit) && hit.collider.TryGetComponent(out Breakable breakable))
        {
            breakable.TakeDamage(toolStr);
        }
    }
}
