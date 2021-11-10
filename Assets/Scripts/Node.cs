using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// TODO: attach to node
public class Node : MonoBehaviour
{
    // TODO: set hover color
    public Color hoverColor;
    public Vector3 positionOffset = new Vector3(0, (float)0.5, 0);
    [Header("Optional")]
    public GameObject turret;
    public MeshRenderer mRend;
    public BuildManager buildManager;
    public int key;
    static int keyLength = 0;

    void Start ()
    {
        buildManager = BuildManager.instance;
        key = keyLength++;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    
    void OnMouseDown ()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;
        //Debug.Log("Clicked on Node.");
        if (!buildManager.CanBuild)
        {
            if(turret != null)
            {
                buildManager.inventory.SelectTower(key);
            }
            return;
        }
        if (turret != null)
        {
            Debug.Log("Can't build there! - TODO: Display on screen.");
            return;
        }
        buildManager.BuildTurretOn(this);
        buildManager.turretToBuild = null;
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
}
