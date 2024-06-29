using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : Interactable
{
    public override void Interact(GameObject gameObject)
    {
        var levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        levelManager.LoadRoom("Floor01");
        gameObject.transform.position = new Vector3(transform.position.x, gameObject.transform.position.y, 0);
    }
}
