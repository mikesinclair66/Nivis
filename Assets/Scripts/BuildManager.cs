using UnityEngine;

// TODO: Attach to game master object
public class BuildManager : MonoBehaviour
{
    // utilizing singleton pattern since only one build manager is needed for all nodes
    public static BuildManager instance;
    public Inventory inventory;

    private TurretBlueprint turretToBuild;
    public int turretToBuildType;
    private Node selectedNode;

    // // TODO: add turret prefab
    // public GameObject standardTurretPrefab;
    // public GameObject missileLauncherPrefab;
    // public GameObject pulsorTurret;

    public Drill drill;
    public GameObject turretGhost;
    public GameObject obj;
    public GameObject forcefieldRange;
    public bool isBuildingTurret;
    private Ray ray;
    private RaycastHit hit;
    //public NodeUI nodeUI;

    // TODO: add select/deselect code for the turret UI, optional depending on how we plan to implement this
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (isBuildingTurret == true)
        {
            if (Physics.Raycast(ray, out hit))
            {
                obj.transform.position = hit.point;
            }
        }
    }

    public bool isTurretSelected { get { return turretToBuild != null; } }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;
        inventory.nodeUI.SetTarget(node);
    }
    public void DeselectNode()
    {
        Debug.Log("Node Deselected");
        selectedNode = null;
        //inventory.nodeUI.Hide();
    }

    public Node GetSelectedNode()
    {

        return selectedNode;
    }

    public void SelectTurretToBuild(TurretBlueprint turret, int turretToBuildType)
    {
        isBuildingTurret = true;
        if (isBuildingTurret == true)
        {
            obj = Instantiate(turretGhost, hit.point, Quaternion.identity);
            Debug.Log("Obj Instantiate");
        }
        turretToBuild = turret;
        if (turretToBuildType == 0)
        {
            float currentScale = turretToBuild.prefab.GetComponent<Turret>().range;
            forcefieldRange = GameObject.Find("FieldRange");
            forcefieldRange.transform.localScale = new Vector3(currentScale, 1f, currentScale);

        }
        if (turretToBuildType == 1)
        {
            float currentScale = turretToBuild.prefab.GetComponent<Turret>().range;
            forcefieldRange = GameObject.Find("FieldRange");
            forcefieldRange.transform.localScale = new Vector3(currentScale, 1f, currentScale);

        }
        if (turretToBuildType == 2)
        {
            float currentScale = turretToBuild.prefab.GetComponent<Pulsor>().pulsorRange;
            forcefieldRange = GameObject.Find("FieldRange");
            forcefieldRange.transform.localScale = new Vector3(currentScale, 1f, currentScale);

        }

        this.turretToBuildType = turretToBuildType;
        DeselectNode();

    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

}
