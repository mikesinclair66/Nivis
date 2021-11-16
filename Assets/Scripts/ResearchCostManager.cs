using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchCostManager : MonoBehaviour
{
    public int killCount = 100;
    public int[,,] researchCost;
    private void Start()
    {
        researchCost = getResearchCosts();//page,branch,upgrades
    }
    
    void Update()
    {
        Text killCountText = GameObject.Find("KillCount").GetComponent<Text>();
        killCountText.text = "Kill Count: " + killCount;
    }

    private static int[,,] getResearchCosts()
    {
        return new int[,,]
        {
            {   // turret 1, path 1
                {5, 10, 15},
                // turret 1, path 2
                {6, 9, 12}
            },
            {   // turret 2, path 1
                {4, 8, 10},
                // turret 2, path 2
                {5, 10, 15}
            },
            {   // turret 3, path 1
                {1, 2, 3},
                // turret 3, path 2
                {4, 5, 6}
            },
        };
    }
    public bool checkIfCanResearch(int _killCount, int turret, int path, int tier)
    {
        if (researchCost[turret, path, tier] > _killCount)
        {
            Debug.Log("Not enough kills to research tech");
            return false;
        }
        else
        {
            Debug.Log("Enough kills to research tech");
            killCount -= _killCount;
            ResearchStation.researched[turret, path, tier] = true;
            return true;
        }
    }
}
