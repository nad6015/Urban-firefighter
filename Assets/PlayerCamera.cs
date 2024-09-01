using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Code for camera rotation referenced from - https://www.youtube.com/watch?v=cOWHojRSGCU
    public float sensitvity = -1.0f;
    private float y = 0;
    private Vector3 rotate;
    private float x = 0;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    // Update is called once per frame
    void Update()
    {
        y = Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1);
        Debug.Log(y);
        x = Mathf.Clamp(Input.GetAxis("Mouse Y"), -45, 45); 
        rotate = new Vector3(x, y * sensitvity, 0);
        transform.eulerAngles = transform.eulerAngles - rotate;
    }
}
