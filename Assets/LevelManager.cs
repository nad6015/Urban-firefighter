using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string nextLevel;
    internal int numOfPeopleToSave;
    internal int numOfFiresToExtinguish;

    void Awake()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponentInChildren<Animator>().gameObject.SetActive(false);

        numOfPeopleToSave = FindObjectsOfType<Civilian>().Length;
        numOfFiresToExtinguish = FindObjectsOfType<Fire>().Length;
    }

    public void IsLevelCompleted()
    {
        Debug.Log(numOfPeopleToSave);
        if (numOfPeopleToSave <= 0 && numOfFiresToExtinguish <=0) {
            GetComponent<BoxCollider>().enabled = true;
            GetComponentInChildren<Animator>().gameObject.SetActive(true);
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

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(nextLevel);
    }
}
