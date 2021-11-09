using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    GameObject panel;
    GameObject towerBtn;
    GameObject damageMod;

    void Start()
    {
        panel = GameObject.Find("Canvas/Hotbar");
        towerBtn = GameObject.Find("Canvas/Hotbar/TowerButton");
        damageMod = GameObject.Find("Canvas/Hotbar/Health/HealthMod");
    }

    public void TakeDamage(int totalHealth)
    {
        damageMod.gameObject.transform.localScale = new Vector3((float)totalHealth / 100.0f, 1, 1);
    }
}
