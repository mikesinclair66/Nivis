using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drill : MonoBehaviour
{
    public float currentMoney = 3000f;
    public float moneyRate = 5.5f;
    public int drillLvl = 1;
    public float upgradeCost = 400f;

    public Text totalMoneyText/*, upgradeCostText*/;
    public GameObject generator, drillSelector;
    Generator gen;

    void Awake()
    {
        gen = generator.GetComponent<Generator>();
        drillSelector = GameObject.Find("Canvas/DrillSelector");
        RectTransform rt = drillSelector.GetComponent<RectTransform>();
        rt.offsetMin = new Vector2(-gameObject.transform.position.x * Screen.width * 0.01085f, rt.offsetMin.y);
        rt.offsetMin = new Vector2(rt.offsetMin.x, gameObject.transform.position.y * Screen.height * 0.16f);
        //rt.offsetMin = new Vector2(rt.offsetMin.x, -offsetY);
    }

    void Update()
    {
        currentMoney += moneyRate * Time.deltaTime;
        totalMoneyText.text = "$"+currentMoney.ToString("0") + "/Lvl: " + drillLvl.ToString();//sector 3 digits by ,
        //upgradeCostText.text = "$" + upgradeCost.ToString();
    }

    public void Upgrade()
    {
        if (drillLvl < 4)
        {
            if (currentMoney > upgradeCost &&
                gen.totalHealth > 30)
            {
                gen.TakeDamage(30);
                currentMoney -= upgradeCost;
                drillLvl++;
                upgradeCost = drillLvl * 300;
                moneyRate = drillLvl * 5f;
            }
        }
    }
}
