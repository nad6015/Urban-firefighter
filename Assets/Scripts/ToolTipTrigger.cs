using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipTrigger : MonoBehaviour
{
    public string tooltipMessage; // The message to display
    public GameObject tooltipObject; // The specific tooltip object to show in world space

    private TooltipManager tooltipManager;

    void Start()
    {
        tooltipManager = FindObjectOfType<TooltipManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tooltipObject.transform.LookAt(Camera.main.transform); // Make the tooltip face the camera
            tooltipManager.ShowTooltip(tooltipMessage);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tooltipManager.HideTooltip();
        }
    }

    // This is for testing purposes
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Simulate OnTriggerEnter with a test object
            tooltipManager.ShowTooltip(tooltipMessage);
            tooltipObject.transform.LookAt(Camera.main.transform);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Simulate OnTriggerExit
            tooltipManager.HideTooltip();
        }
    }
}
