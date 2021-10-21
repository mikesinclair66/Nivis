using UnityEngine;

// TODO: Attach to game master object
public class BuildManager : MonoBehaviour
{
    // utilizing singleton pattern since only one build manager is needed for all nodes
    public static BuildManager instance;

    // TODO: add turret prefab
    public GameObject standardTurretPrefab;
    public GameObject missileLauncherPrefab;
    void Awake ()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }
    
    public TurretBlueprint turretToBuild;
    public Drill drill;
    public bool CanBuild { get { return turretToBuild != null;  } }

    public void BuildTurretOn (Node node)
    {
        if (drill.currentMoney < turretToBuild.cost )
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        drill.currentMoney -= turretToBuild.cost;

       GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(),Quaternion.identity);
       node.turret = turret;

        Debug.Log("Turret build! Money left: " + drill.currentMoney);
    }

    public void SelectTurretToBuild (TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
