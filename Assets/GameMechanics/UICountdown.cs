using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICountdown : MonoBehaviour
{
    public float countdownTime = 30;
    public String startingText;
    public event Action onCountdownFinished;

    private bool hasStarted = false;
    private TextMeshProUGUI countdownDisplay;
    void Start()
    {
        countdownDisplay  = GetComponent<TextMeshProUGUI>();
        Debug.Log(countdownDisplay);
        countdownDisplay.text = startingText;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasStarted && countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            countdownDisplay.text = Math.Round(countdownTime).ToString();
        }

        if(countdownTime < 0)
        {
            hasStarted = false;
            onCountdownFinished();
        }
    }

    public void StartCountdown()
    {
        hasStarted = true;
    }
}
