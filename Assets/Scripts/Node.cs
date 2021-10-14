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
    
    private GameObject turret;
    private Renderer rend;
    private Color startColor;
    
    void Start ()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    
    void OnMouseDown ()
    {
        if (turret != null)
        {
            Debug.Log("Can't build there! - TODO: Display on screen.");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }
    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }
    void OnMouseExit ()
    {
        rend.material.color = startColor;
    }
}
