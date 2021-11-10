using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    List<GameObject> turrets;
    List<int> turretType, upgradeLvl, upgradePrimary, nodeKey;

    public Inventory()
    {
        turrets = new List<GameObject>();
        turretType = new List<int>();
        upgradeLvl = new List<int>();
        upgradePrimary = new List<int>();
        nodeKey = new List<int>();
    }

    public void Add(GameObject turret, int type, int nodeKey)
    {
        turrets.Add(turret);
        turretType.Add(type);
        upgradeLvl.Add(0);
        upgradePrimary.Add(-1);
        this.nodeKey.Add(nodeKey);
    }

    public void SelectTower(int nodeKey)
    {
        int towerNo = -1;

        for(int i = 0; i < turrets.Count; i++)
        {
            if (this.nodeKey[i] == nodeKey)
                towerNo = i;
        }

        if (towerNo == -1)
        {
            Debug.LogError("No tower was seleced through Inventory.SelectTower(i)");
            return;
        }

        Debug.Log("Tower #" + towerNo + " selected.");
    }
}
