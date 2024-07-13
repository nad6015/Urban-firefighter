using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public PlayerCharacter character;
    public Image bar;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = (float)character.HP / character.maxHP;
    }
}
