using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drill : MonoBehaviour
{
    public float currentMoney;
    public float moneyRate;
    public int drillLvl;
    public float upgradeCost;

    private GameObject generator;

    public Text totalMoneyText, upgradeTier, upgradeCostText;

    void Start()
    {
        currentMoney = 300f;
        drillLvl = 1;
        moneyRate = 5.5f;
        upgradeCost = 400f;

        generator = GameObject.Find("END");
    }

    void Update()
    {
        currentMoney += moneyRate * Time.deltaTime;
        totalMoneyText.text = "$"+currentMoney.ToString("0");
        upgradeTier.text = "Lvl: " + drillLvl.ToString();
        upgradeCostText.text = "$" + upgradeCost.ToString();
    }

    public void Upgrade()
    {
        if (drillLvl < 4)
        {
            if (currentMoney > upgradeCost &&
                generator.GetComponent<Generator>().totalHealth > 30)
            {
                generator.GetComponent<Generator>().TakeDamage(30);
                currentMoney -= upgradeCost;
                drillLvl++;
                upgradeCost = drillLvl * 300;
                moneyRate = drillLvl * 5f;
                //generator.GetComponent<Generator>().totalHealth -= 30;
            }
        }
    }


}
