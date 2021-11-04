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
    public bool isUpgraded = false;

    void Start ()
    {
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
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
            }
        }
    }
    
    public void SellTurret ()
    {
        if (turret != null)
        {
            drill.currentMoney += turretBlueprint.sellValue;

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

    public void UpgradeTurret()
    {
        if (drill.currentMoney < turretBlueprint.cost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        drill.currentMoney -= turretBlueprint.cost;

        Destroy(turret);
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);

        if (_turret != null)
        {
            turret = _turret;
            Debug.Log("Turret upgraded! Money left: " + drill.currentMoney);
            isUpgraded = true;
        }
    }
}
