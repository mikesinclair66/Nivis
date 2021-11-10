using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;

    BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        //Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(standardTurret, 0);
    }

    public void SelectMissileLauncher()
    {
        //Debug.Log("Missile Launcher Selected");
        buildManager.SelectTurretToBuild(missileLauncher, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
