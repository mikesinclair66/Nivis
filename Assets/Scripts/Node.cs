using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset = new Vector3(0, (float)0.5, 0);
    // TODO: Add drill to node prefab
    public Drill drill;
    [Header("Optional")]
    public GameObject turret;
    public MeshRenderer mRend;
    public BuildManager buildManager;
    public TurretBlueprint turretBlueprint;
    private int currentUpgradePath;
    private int currentUpgradeTier;
    private int maxUpgradeTier = 3;

    void Start ()
    {
        buildManager = BuildManager.instance;
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
    public int getMaxUpgradeTier()
    {
        return maxUpgradeTier;
    }
    
    void BuildTurret(TurretBlueprint blueprint)
    {
        if (blueprint != null)
        {
            if (drill.currentMoney < blueprint.cost)
            {
                Debug.Log("Not enough money to build that!");
                return;
            }

            drill.currentMoney -= blueprint.cost;

            turretBlueprint = blueprint;

            GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);

            if (_turret != null)
            {
                turret = _turret;
                Debug.Log("Turret build! Money left: " + drill.currentMoney);
                currentUpgradePath = 0;
                currentUpgradeTier = 0;
            }
        }
    }
    
    public void SellTurret ()
    {
        if (turret != null)
        {
            drill.currentMoney += turretBlueprint.sellValue;

            currentUpgradePath = 0;
            currentUpgradeTier = 0;

            Destroy(turret);
            turretBlueprint = null;
        }
    }
    
    void OnMouseDown ()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;
        Debug.Log("Clicked on Node.");
        if (!buildManager.CanBuild)
        {
            return;
        }
        if (turret != null)
        {
            Debug.Log("Can't build there! - TODO: Display on screen.");
            return;
        }
        BuildTurret(buildManager.GetTurretToBuild());
        // Deselects turret to build in build manager after building a turret
        buildManager.SelectTurretToBuild(null);
    }
    void OnMouseEnter()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;
        if (!buildManager.CanBuild)
            return;
        mRend.enabled = true;
    }
        
    void OnMouseExit ()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;
        mRend.enabled = false;
    }

    public void UpgradeTurret(int path, int tier)
    {
        List<UpgradeBlueprint> requestedPath = getUpgradeBlueprints(path);
        if (!canUpgrade(path, tier))
        {
            return;
        }
        drill.currentMoney -= turretBlueprint.cost;

        Destroy(turret);
        GameObject _turret = Instantiate(requestedPath[tier].prefab, GetBuildPosition(), Quaternion.identity);
        
        if (_turret != null)
        {
            turret = _turret;
            Debug.Log("Turret upgraded! Money left: " + drill.currentMoney);
            currentUpgradePath = path;
            currentUpgradeTier = tier;
        }
    }

    public bool canUpgrade(int path, int tier)
    {
        List<UpgradeBlueprint> requestedPath = getUpgradeBlueprints(path);
        
        if (requestedPath == null)
        {
            Debug.Log("Invalid path requested! Path must be 1 or 2.");
            return false;
        }

        if (!validateRequestedPath(path) || !validateRequestedTier(tier))
        {
            Debug.Log("Invalid path or tier. Cannot upgrade.");
            return false;
        }
        
        if (drill.currentMoney < requestedPath[tier].cost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return false;
        }
        return true;
    }

    private List<UpgradeBlueprint> getUpgradeBlueprints(int path)
    {
        List<UpgradeBlueprint> requestedPath;
        if (path == 1)
        {
            requestedPath = turretBlueprint.upgradeBlueprintsPath1;
        }
        else if (path == 2)
        {
            requestedPath = turretBlueprint.upgradeBlueprintsPath2;
        }
        else
        {
            Debug.Log("Cannot get upgrade blueprint of requested path");
            return null;
        }
        return requestedPath;
    }

    private bool validateRequestedPath(int path)
    {
        if (currentUpgradePath == 0)
        {
            return path == 1 || path == 2;
        }
        else
        {
            return currentUpgradePath == path;
        }
    }

    private bool validateRequestedTier(int tier)
    {
        if (tier > maxUpgradeTier)
        {
            return false;
        }
        else
        {
            return tier == currentUpgradeTier + 1;
        }
    }
}
