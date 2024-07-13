using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currentFloor;
    public Dictionary<string, GameObject> floors;

    private Vector3 origin = Vector3.zero;

    public void Start()
    {
        floors = new Dictionary<string, GameObject>
        {
            { currentFloor.name, currentFloor }
        };
    }

    internal void LoadRoom(GameObject room, Vector3 roomPos)
    {
        if (floors.ContainsKey(room.name))
        {
            ToggleCurrentRoom(floors[room.name]);
            return;
        }
        var newRoom = Instantiate(room, roomPos, Quaternion.identity);
        floors.Add(room.name, newRoom);
        ToggleCurrentRoom(newRoom);
    }

    internal void LoadRoom(string roomName)
    {
        var room = floors[roomName];
        ToggleCurrentRoom(room);
    }

    private void ToggleCurrentRoom(GameObject room)
    {
        room.SetActive(true);
        currentFloor.SetActive(false);
        currentFloor = room;
    }
}
