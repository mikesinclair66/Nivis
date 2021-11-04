using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    // TODO: hook up public variables with game objects in editor
    public GameObject ui;

    public Text upgradeCost;
    public Text sellAmount;
    public Button upgradeButton;

    private Node target;

    public void SetTarget (Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        } else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.sellValue;
        
        ui.SetActive(true);
    }

    public void Hide ()
    {
        ui.SetActive(false);
    }

    // TODO: hook upgrade button in turret UI to this upgrade function
    public void Upgrade ()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
    
    public void Sell ()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

}