using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Attach to game master object
public class BuildManager : MonoBehaviour
{
    // utilizing singleton pattern since only one build manager is needed for all nodes
    public static BuildManager instance;

    // TODO: add turret prefab
    public GameObject standardTurretPrefab;
    private GameObject turretToBuild;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
