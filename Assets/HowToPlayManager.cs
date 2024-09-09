using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayManager : MonoBehaviour
{
    public GameObject howToPlay;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Level_2")
        {
            Close();
        }
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            howToPlay.SetActive(!howToPlay.activeSelf);
        }
    }

    public void Close()
    {
        howToPlay.SetActive(false);
    }
}
