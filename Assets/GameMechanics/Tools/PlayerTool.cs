using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTool : MonoBehaviour
{
    public float toolStr;
    public AudioSource useSFX;
    public Transform usePoint; // Point from where the axe swing will be calculated

    public virtual void Use() { Debug.Log("This shouldn't be be called.");  }

    public virtual void StopUsing()    {     }
}
