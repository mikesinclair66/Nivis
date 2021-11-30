using System.Collections.Generic;
using UnityEngine;

/**
 * Serializing a UpgradePath for Shop.cs to use. UpgradePath is just a list for unity to serialize.
 */
[System.Serializable]
public class UpgradePath
{
    public List<UpgradeBlueprint> upgrades;
}