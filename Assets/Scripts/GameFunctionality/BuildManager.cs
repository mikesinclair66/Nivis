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

    public Drill drill;
    public GameObject turretGhost;
    public GameObject obj;
    public GameObject forcefieldRange;
    public bool isBuildingTurret;
    private Ray ray;
    private RaycastHit hit;
    private GameObject tempObj;

    /**
     * Instantiate the BuildManager upon starting the game
     */
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
                if (obj != null)
                    obj.transform.position = hit.point;
            }
        }

        if (obj != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(tempObj);
                isBuildingTurret = false;
            }
        }
    }

    public bool isTurretSelected { get { return turretToBuild != null; } }

    /**
     * BuildManager handles checking what node is selected. Used for upgrading and selling a turret.
     */
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

    /**
     * BuildManager handles checking what turret is selected to build. Called by Shop.cs
     */
    public void SelectTurretToBuild(TurretBlueprint turret, int turretToBuildType)
    {
        isBuildingTurret = true;
        if (isBuildingTurret == true)
        {
            obj = Instantiate(turretGhost, hit.point, Quaternion.identity);
            tempObj = obj;
            Debug.Log("Obj Instantiate");
            tempObj.GetComponent<RangeIndicator>().ghostRangeIndicator(tempObj, turretToBuildType);
        }
        turretToBuild = turret;
        this.turretToBuildType = turretToBuildType;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

}
