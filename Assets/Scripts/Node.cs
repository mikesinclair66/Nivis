using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start ()
    {
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    
    void OnMouseDown ()
    {
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
        buildManager.BuildTurretOn(this);
    }
    void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
        {
            return;
        }
        mRend.enabled = true;
    }
    void OnMouseExit ()
    {
        mRend.enabled = false;
    }
}
