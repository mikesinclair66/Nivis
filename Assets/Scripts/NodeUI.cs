using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    // TODO: hook up public variables with game objects in editor
    public GameObject ui;

    public Text upgradeCostPath1;
    public Text upgradeCostPath2;
    public Text sellAmount;
    public Button upgradeButton1;
    public Button upgradeButton2;

    private Node target;

    public void SetTarget (Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        applyUpgradeText();

        sellAmount.text = "$" + target.turretBlueprint.sellValue;
        
        ui.SetActive(true);
    }

    private void applyUpgradeText()
    {
        int upgradeTier = target.getCurrentUpgradeTier();
        int upgradePath = target.getCurrentUpgradePath();
        UpgradePath path1 = target.turretBlueprint.paths[0];
        UpgradePath path2 = target.turretBlueprint.paths[1];
        if (upgradeTier == 0)
        {
            int path1cost = target.turretBlueprint.paths[0].upgrades[0].cost;
            int path2cost = target.turretBlueprint.paths[1].upgrades[0].cost;
            upgradeCostPath1.text = "$" + path1cost;
            upgradeCostPath2.text = "$" + path2cost;
            upgradeButton1.interactable = true;
            upgradeButton2.interactable = true;
        }
        else if (upgradeTier == target.getMaxUpgradeTier())
        {
            if (upgradePath == 1)
            {
                upgradeCostPath1.text = "DONE";
                upgradeButton1.interactable = false;
            }

            if (upgradePath == 2)
            {
                upgradeCostPath2.text = "DONE";
                upgradeButton2.interactable = false;
            }
        }
        else
        {
            if (upgradePath == 1)
            {
                int path1cost = path1.upgrades[upgradeTier].cost;
                upgradeCostPath1.text = "$" + path1cost;
                upgradeCostPath2.text = "UNAVAILABLE";
                upgradeButton2.interactable = false;
            }

            if (upgradePath == 2)
            {
                int path2cost = path2.upgrades[upgradeTier].cost;
                upgradeCostPath2.text = "$" + path2cost;
                upgradeCostPath1.text = "UNAVAILABLE";
                upgradeButton1.interactable = false;
            }
        }
    }

    public void Hide ()
    {
        ui.SetActive(false);
    }

    // TODO: hook upgrade button in turret UI to this upgrade function
    public void Upgrade (int path)
    {
        int tier = target.getCurrentUpgradeTier() + 1;
        target.UpgradeTurret(path, tier);
        BuildManager.instance.DeselectNode();
    }
    
    public void Sell ()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

}