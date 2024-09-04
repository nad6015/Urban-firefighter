using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // For restarting or quitting the game

public class WinLoose : MonoBehaviour
{
    // Reference to the UI panels for win and lose screens
    public GameObject winScreen;
    public GameObject loseScreen;

    // A flag to track if the game is over (either win or lose)
    private bool gameIsOver = false;

    void Start()
    {
        // Ensure that both screens are hidden at the start of the game
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    // This method will be called when the player wins the game
    public void WinGame()
    {
        if (!gameIsOver)  // Ensure the win/lose screen is shown only once
        {
            gameIsOver = true;
            winScreen.SetActive(true);  // Show the win screen
            Time.timeScale = 0f;        // Pause the game 
        }
    }

    // This method will be called when the player loses the game
    public void LoseGame()
    {
        if (!gameIsOver)  // Ensure the win/lose screen is shown only once
        {
            gameIsOver = true;
            loseScreen.SetActive(true);  // Show the lose screen
            Time.timeScale = 0f;         // Pause the game
        }
    }

    // Call this to restart the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f;  // Resume normal time scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
    }

    // Call this to quit the game
    public void QuitGame()
    {
        Application.Quit();  // Quit the application
    }

    // // this method is for testing purposes only
    // void Update()
    // {
    //     // Press 'W' to simulate winning the game
    //     if (Input.GetKeyDown(KeyCode.W))
    //     {
    //         WinGame();
    //     }

    //     // Press 'L' to simulate losing the game
    //     if (Input.GetKeyDown(KeyCode.L))
    //     {
    //         LoseGame();
    //     }
    // }
}
