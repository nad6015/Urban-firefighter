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


    void Awake()
    {
        GetComponent<BoxCollider>().enabled = false;
        indicator.SetActive(false);

        numOfPeopleToSave = FindObjectsOfType<Civilian>().Length;
        numOfFiresToExtinguish = FindObjectsOfType<Fire>().Length;
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

    private void OnTriggerEnter(Collider other)
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = Vector3.zero;
        SceneManager.LoadScene(nextLevel);
    }
}
