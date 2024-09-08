using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string nextLevel;
    public GameObject indicator;
    internal int numOfPeopleToSave;
    internal int numOfFiresToExtinguish;
    public bool shouldEndGame = false;


    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        IsLevelCompleted(); // for testing purposes
        ResetObjectives();
        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Level_2") {
            shouldEndGame = true;
        }
    }

    public void IsLevelCompleted()
    {
        if (numOfPeopleToSave <= 0 && numOfFiresToExtinguish <=0) {
            GetComponent<BoxCollider>().enabled = true;
            indicator.SetActive(true);
        }
    }

    public void DecreaseCivilianCount()
    {
        numOfPeopleToSave--;
    }

    public void DecreaseFireCount()
    {
        numOfFiresToExtinguish--;
    }

    public void ResetObjectives()
    {
        GetComponent<BoxCollider>().enabled = false;
        indicator.SetActive(false);

        numOfPeopleToSave = FindObjectsOfType<Civilian>().Length;
        numOfFiresToExtinguish = FindObjectsOfType<Fire>().Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = Vector3.zero;
        SceneManager.LoadScene(nextLevel);

        if(shouldEndGame)
        {
            GetComponent<WinLoose>().WinGame();
        }
    }
}
