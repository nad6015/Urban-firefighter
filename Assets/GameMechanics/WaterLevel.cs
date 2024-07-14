using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevel : MonoBehaviour
{
    public FireExtinguisher extinguisher;
    public Image bar;

    void Update()
    {
        bar.fillAmount = (float)extinguisher.WaterSupply / extinguisher.maxWaterSupply;
    }
}
