using UnityEngine;

/**
 * Serializing a UpgradeBlueprint for Shop.cs to use. UpgradeBlueprint should be an upgraded turret.
 */
[System.Serializable]
public class UpgradeBlueprint
{
    public GameObject prefab;
    public int cost;
    public int sellValue;
}