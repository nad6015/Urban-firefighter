using UnityEngine;
using TMPro;

public class LivesSavedController : MonoBehaviour
{
    public TextMeshProUGUI livesSavedText; // Reference to the TextMeshProUGUI component
    private int livesSaved = 0;

    void Start()
    {
        UpdateLivesSavedText();
    }

    public void SaveLife()
    {
        livesSaved++;
        UpdateLivesSavedText();
    }

    private void UpdateLivesSavedText()
    {
        livesSavedText.text = "Lives Saved: " + livesSaved;
    }

    // // test: with the press of the letter "S", the number of lives saved is increased by 1
    // // to test uncomment the Update() method below

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.S))
    //     {
    //         SaveLife();
    //     }
    // }

}
