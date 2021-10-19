using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drill : MonoBehaviour
{
    public float currentMoney;
    public float moneyRate;
    public int upgradeTier;
    public float upgradeCost;

    private GameObject generator;

    public Text totalMoneyText;

    void Start()
    {
        currentMoney = 100f;
        upgradeTier = 1;
        moneyRate = 5f;
        upgradeCost = 100f;

        generator = GameObject.Find("END");
    }

    void Update()
    {
        currentMoney += moneyRate * Time.deltaTime;
        totalMoneyText.text = currentMoney.ToString("0");
    }

    public void Upgrade()
    {
        if (upgradeTier < 4)
        {
            if (currentMoney > upgradeCost)
            {
                currentMoney -= upgradeCost;
                upgradeTier++;
                moneyRate++;
                Debug.Log("Drill tier: " + upgradeTier);
                if (generator.GetComponent<Generator>().totalHealth > 30)
                {
                    generator.GetComponent<Generator>().totalHealth -= 30;
                }
            }
        }
    }


}
