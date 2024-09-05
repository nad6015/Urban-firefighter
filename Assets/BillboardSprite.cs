using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BillboardSprite : MonoBehaviour
{
    // Billboarding code referenced from - https://discussions.unity.com/t/how-i-can-create-an-sprite-that-always-look-at-the-camera/16891
    void LateUpdate()
    {
       transform.forward += Camera.main.transform.forward;
    }
}
