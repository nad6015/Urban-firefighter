using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public Text tooltipText;
    public GameObject tooltipObject;

    void Start()
    {
        HideTooltip(); // Hide the tooltip initially
    }

    public void ShowTooltip(string message)
    {
        tooltipText.text = message;
        tooltipObject.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltipObject.SetActive(false);
    }
}
