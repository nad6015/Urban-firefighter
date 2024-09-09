using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Call this to quit the game
    public void Quit()
    {
        Application.Quit(); // Quit the application
    }

    // Call this to restart the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f;  // Resume normal time scale
        SceneManager.LoadScene("Level_1");  // Start the game from level 1
    }
}
