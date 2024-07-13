using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public int HP = 30;
    public int maxHP = 30;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            HP -= 6;
            var newPos = transform.position;
            newPos.x -= 0.5f;
            transform.position = newPos;
        }
    }
}
