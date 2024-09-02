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
        
        if (Physics.Raycast(pc.transform.position, pc.movement.GetModelForward() * 2f, out RaycastHit hit, 3f, LayerMask.NameToLayer("breakable")) && hit.collider.TryGetComponent(out Breakable breakable))
        {
            breakable.TakeDamage(toolStr);
        }
    }
}
