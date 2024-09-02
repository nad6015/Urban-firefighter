using UnityEngine;
using TMPro;

public class LivesSavedController : MonoBehaviour
{
    public TextMeshProUGUI livesSavedText; // Reference to the TextMeshProUGUI component
    private int livesSaved = 0;
    private int numOfPeopleToSave;

    private void Awake()
    {
        numOfPeopleToSave = FindObjectsOfType<Civilian>().Length;
    }

    void Start()
    {
        UpdateLivesSavedText();
    }

    public void SaveLife()
    {
        livesSaved++;
        if(livesSaved >= numOfPeopleToSave)
        {
            livesSaved = numOfPeopleToSave;
        }
        UpdateLivesSavedText();
    }

    private void UpdateLivesSavedText()
    {
        livesSavedText.text = "Lives Saved: " + livesSaved + "/" + numOfPeopleToSave;
    }
}
