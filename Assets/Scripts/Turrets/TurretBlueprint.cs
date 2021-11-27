using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public List<UpgradePath> paths;
    public int cost;
    public int sellValue;

}