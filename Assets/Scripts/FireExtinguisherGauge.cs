using UnityEngine;
using UnityEngine.UI;

public class FireExtinguisherGauge : MonoBehaviour
{
    public RectTransform fireExtinguisherRectTransform; // Reference to the RectTransform component
    
    private float currentLevel;
    private float maxLevel = 100f;

    void Start()
    {
        // Initialize level
        currentLevel = maxLevel;
        UpdateFireExtinguisherLevel();
    }

    public void UseExtinguisher(float amount)
    {
        // Reduce level
        currentLevel -= amount;
        if (currentLevel < 0) currentLevel = 0;
        UpdateFireExtinguisherLevel();
    }

    public void RefillExtinguisher(float amount)
    {
        // Increase level
        currentLevel += amount;
        if (currentLevel > maxLevel) currentLevel = maxLevel;
        UpdateFireExtinguisherLevel();
    }

    private void UpdateFireExtinguisherLevel()
    {
        // Update the fire extinguisher level UI
        float levelPercent = currentLevel / maxLevel;
        fireExtinguisherRectTransform.localScale = new Vector3(levelPercent, 1, 1);
    }

    public void SetMaxLevel(float level)
    {
        maxLevel = level;
        currentLevel = level;
        UpdateFireExtinguisherLevel();
    }

    public void SetLevel(float level)
    {
        currentLevel = level;
        UpdateFireExtinguisherLevel();
    }
}