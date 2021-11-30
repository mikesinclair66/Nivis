using System.Collections.Generic;
using UnityEngine;

/**
 * Serializing a TurretBlueprint for Shop.cs to use. TurretBlueprint should be base turret only.
 */
[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public List<UpgradePath> paths;
    public int cost;
    public int sellValue;

}