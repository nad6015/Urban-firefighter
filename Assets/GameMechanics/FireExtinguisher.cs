using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public int waterStr = 2;
    internal int maxWaterSupply = 15;
    private int waterSupply = 15;

    public int WaterSupply { get => waterSupply; }

    internal void decreaseWaterSupply()
    {
        waterSupply -= waterStr;
    }
}
