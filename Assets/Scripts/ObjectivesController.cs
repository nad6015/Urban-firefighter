using UnityEngine;
using TMPro;

public class ObjectivesController : MonoBehaviour
{
    public TextMeshProUGUI livesSavedText; // Reference to the TextMeshProUGUI component
    public TextMeshProUGUI firesExtinguishedText; // Reference to the TextMeshProUGUI component

    private int livesSaved = 0;
    private int firesExtinguished = 0;

    private int totalCivilians = 0;
    private int totalFires = 0;
    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Start()
    {
        totalCivilians = levelManager.numOfPeopleToSave;
        totalFires = levelManager.numOfFiresToExtinguish;
        UpdateLivesSavedText();
        UpdateFiresExtinguishedText();
    }

    public void SaveLife()
    {
        livesSaved++;
        if (livesSaved >= totalCivilians)
        {
            livesSaved = totalCivilians;
        }
        UpdateLivesSavedText();
    }

    private void UpdateLivesSavedText()
    {
        livesSavedText.text = "Lives Saved: " + livesSaved + "/" + totalCivilians;
    }

    public void ExtinguishFire()
    {
        firesExtinguished++;
        if (firesExtinguished >= totalFires)
        {
            firesExtinguished = totalFires;
        }
        UpdateFiresExtinguishedText();
    }

    private void UpdateFiresExtinguishedText()
    {
        firesExtinguishedText.text = "Fires Extinguished: " + firesExtinguished + "/" + totalFires;
    }
}
