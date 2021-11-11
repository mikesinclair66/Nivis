using UnityEngine;

// TODO: Attach to game master object
public class BuildManager : MonoBehaviour
{
    // utilizing singleton pattern since only one build manager is needed for all nodes
    public static BuildManager instance;
    
    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    // TODO: add turret prefab
    public GameObject standardTurretPrefab;
    public GameObject missileLauncherPrefab;
    public GameObject MeleeTurretPrefab;

    public Drill drill;
    public NodeUI nodeUI;
    
    // TODO: add select/deselect code for the turret UI, optional depending on how we plan to implement this
    void Awake ()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }
    
    public bool isTurretSelected { get { return turretToBuild != null;  } }

    public void SelectNode (Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    public void SelectTurretToBuild (TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }
    
    public TurretBlueprint GetTurretToBuild ()
    {
        return turretToBuild;
    }
    
}
