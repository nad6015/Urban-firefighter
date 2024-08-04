using UnityEngine;
using UnityEngine.UI;

public class FireExtinguisherController : MonoBehaviour
{
    public RectTransform fireExtinguisherRectTransform; // Reference to the RectTransform component
    private float currentLevel;
    private float maxLevel = 100f;

    void Start()
    {
        // Initialize level
        currentLevel = maxLevel;
        UpdateFireExtinguisherLevel();
        Debug.Log("Fire extinguisher level initialized: " + currentLevel);
    }

    public void UseExtinguisher(float amount)
    {
        // Reduce level
        currentLevel -= amount;
        if (currentLevel < 0) currentLevel = 0;
        UpdateFireExtinguisherLevel();
        Debug.Log("Used extinguisher: " + amount + ", Current Level: " + currentLevel);
    }

    public void RefillExtinguisher(float amount)
    {
        // Increase level
        currentLevel += amount;
        if (currentLevel > maxLevel) currentLevel = maxLevel;
        UpdateFireExtinguisherLevel();
        Debug.Log("Refilled extinguisher: " + amount + ", Current Level: " + currentLevel);
    }

    private void UpdateFireExtinguisherLevel()
    {
        // Update the fire extinguisher level UI
        float levelPercent = currentLevel / maxLevel;
        fireExtinguisherRectTransform.localScale = new Vector3(levelPercent, 1, 1);
        Debug.Log($"Fire extinguisher level updated: {levelPercent * 100}%");
    }

    public void SetMaxLevel(float level)
    {
        maxLevel = level;
        currentLevel = level;
        UpdateFireExtinguisherLevel();
        Debug.Log("Max level set: " + maxLevel);
    }

    public void SetLevel(float level)
    {
        currentLevel = level;
        UpdateFireExtinguisherLevel();
        Debug.Log("Level set: " + currentLevel);
    }

    // // test: with the press of the letter "U" the extinguisher level will drop and with "R" letter increase
    // // to test uncomment the Update() method below

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.U))
    //     {
    //         UseExtinguisher(10);
    //     }

    //     if (Input.GetKeyDown(KeyCode.R))
    //     {
    //         RefillExtinguisher(10);
    //     }
    // }
}