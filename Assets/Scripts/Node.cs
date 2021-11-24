using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset = new Vector3(0, (float)0.5, 0);
    [Header("Optional")]
    public GameObject turret;
    public MeshRenderer mRend;
    public BuildManager buildManager;
    public TurretBlueprint turretBlueprint;
    private UpgradePath upgradePath;
    private int currentUpgradePath;
    private int currentUpgradeTier;
    public int key;
    static int keyLength;

    void Start()
    {
        buildManager = BuildManager.instance;
        key = keyLength++;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    public int getCurrentUpgradePath()
    {
        return currentUpgradePath;
    }
    public int getCurrentUpgradeTier()
    {
        return currentUpgradeTier;
    }
    public List<int> getMaxUpgradeTier()
    {
        List<int> maxUpgrades = new List<int>();
        List<UpgradePath> paths = turretBlueprint.paths;
        foreach (UpgradePath path in paths)
        {
            maxUpgrades.Add(path.upgrades.Count);
        }
        return maxUpgrades;
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (blueprint != null)
        {
            Debug.Log("drill money: " + buildManager.drill.currentMoney);
            Debug.Log("blueprint cost: " + blueprint.cost);
            if (buildManager.drill.currentMoney < blueprint.cost)
            {
                Debug.Log("Not enough money to build that!");
                return;
            }

            buildManager.drill.currentMoney -= blueprint.cost;

            turretBlueprint = blueprint;

            GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
            buildManager.inventory.Add(_turret, buildManager.turretToBuildType, key);

            if (_turret != null)
            {
                turret = _turret;
                Debug.Log("Turret build! Money left: " + buildManager.drill.currentMoney);
                currentUpgradeTier = 0;
            }
        }
    }

    public void SellTurret()
    {
        if (turret != null)
        {
            //buildManager.drill.currentMoney += turretBlueprint.sellValue;

            Destroy(turret);
            turretBlueprint = null;
            upgradePath = null;
            currentUpgradePath = 0;
            currentUpgradeTier = 0;
        }
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        Debug.Log("Clicked on Node.");
        if (!buildManager.isTurretSelected && turret == null)
        {
            buildManager.inventory.SelectTower(-1);
            return;
        }
        Abilities abilities = GameObject.Find("AttackButtons").GetComponent<Abilities>();
        if (abilities.reenableTurretIsRequested && turret != null)
        {
            abilities.reenableTurret(turret);
        }

        abilities.reenableTurretIsRequested = false;
        if (turret != null)
        {
            buildManager.SelectNode(this);
            buildManager.inventory.SelectTower(key);
            return;
        }
        BuildTurret(buildManager.GetTurretToBuild());
        buildManager.inventory.SelectTower(-1);
        
        // Deselects turret to build in build manager after building a turret
        // buildManager.SelectTurretToBuild(null);
    }
    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.isTurretSelected && turret == null)
            return;
        mRend.enabled = true;
    }

    void OnMouseExit()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        mRend.enabled = false;
    }

    public void UpgradeTurret(int path, int tier)
    {
        UpgradePath requestedPath = getUpgradePath(path);
        if (!canUpgrade(path, tier))
        {
            return;
        }
        buildManager.drill.currentMoney -= requestedPath.upgrades[tier-1].cost;

        Destroy(turret);
        GameObject _turret = Instantiate(requestedPath.upgrades[tier - 1].prefab, GetBuildPosition(), Quaternion.identity);

        if (_turret != null)
        {
            turret = _turret;
            Debug.Log("Turret upgraded! Money left: " + buildManager.drill.currentMoney);
            upgradePath = requestedPath;
            currentUpgradePath = path;
            currentUpgradeTier = tier;
        }
    }

    public bool canUpgrade(int path, int tier)
    {
        UpgradePath requestedPath = getUpgradePath(path);

        if (requestedPath == null)
        {
            Debug.Log("Invalid path requested! Path must be 1 or 2.");
            return false;
        }

        /*if (!hasResearch(path, tier))
        {
            Debug.Log("Research for this upgrade has not been acquired.");
            return false;
        }*/

        if (!validateRequestedPath(path) || !validateRequestedTier(requestedPath, tier))
        {
            Debug.Log("Invalid path or tier. Cannot upgrade.");
            return false;
        }

        if (buildManager.drill.currentMoney < requestedPath.upgrades[tier - 1].cost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return false;
        }
        return true;
    }

    private UpgradePath getUpgradePath(int path)
    {
        UpgradePath requestedPath = turretBlueprint.paths[path - 1];
        if (requestedPath == null)
        {
            Debug.Log("Cannot get upgrade blueprint of requested path");
            return null;
        }
        return requestedPath;
    }

    private bool validateRequestedPath(int path)
    {
        if (upgradePath == null)
        {
            return path == 1 || path == 2;
        }
        else
        {
            return currentUpgradePath == path;
        }
    }

    private bool validateRequestedTier(UpgradePath path, int tier)
    {
        if (tier > path.upgrades.Count)
        {
            return false;
        }
        else
        {
            return tier == currentUpgradeTier + 1;
        }
    }
}