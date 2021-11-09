using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    List<GameObject> turrets;
    List<int> turretType, upgradeLvl, upgradePrimary;

    public Inventory()
    {
        turrets = new List<GameObject>();
        turretType = new List<int>();
        upgradeLvl = new List<int>();
        upgradePrimary = new List<int>();
    }

    public void Add(GameObject turret, int type)
    {
        turrets.Add(turret);
        turretType.Add(type);
        upgradeLvl.Add(0);
        upgradePrimary.Add(-1);
    }
}
