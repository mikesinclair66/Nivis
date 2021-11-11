using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public List<UpgradeBlueprint> upgradeBlueprintsPath1;
    public List<UpgradeBlueprint> upgradeBlueprintsPath2;
    public int cost;
    public int sellValue;

}