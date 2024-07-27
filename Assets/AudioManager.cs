using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set;}
    
    void Awake() {
        if(Instance != null && Instance == this) {
            Destroy(this);
        }else {
            Instance = this;
        }
    }

}
