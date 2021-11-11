﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drill : MonoBehaviour
{
    public float currentMoney;
    public float moneyRate;
    public int drillLvl;
    public float upgradeCost;

    public Text totalMoneyText, upgradeCostText;

    void Start()
    {
        currentMoney = 3000f;
        drillLvl = 1;
        moneyRate = 5.5f;
        upgradeCost = 400f;
    }

    void Update()
    {
        currentMoney += moneyRate * Time.deltaTime;
        totalMoneyText.text = "$"+currentMoney.ToString("0") + "/Lvl: " + drillLvl.ToString();//sector 3 digits by ,
        upgradeCostText.text = "$" + upgradeCost.ToString();
    }

    public void Upgrade()
    {
        if (drillLvl < 4)
        {
            if (currentMoney > upgradeCost &&
                Generator.totalHealth > 30)
            {
                Generator.TakeDamage(30);
                currentMoney -= upgradeCost;
                drillLvl++;
                upgradeCost = drillLvl * 300;
                moneyRate = drillLvl * 5f;
            }
        }
    }


}
