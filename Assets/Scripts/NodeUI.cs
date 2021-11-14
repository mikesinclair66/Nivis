using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{

    // TODO: hook up public variables with game objects in editor
    public GameObject ui;

    public Text upgradeCostPath1;
    public Text upgradeCostPath2;
    public Text sellAmount;
    public Button upgradeButton1;
    public Button upgradeButton2;

    private Node target;

    public void SetTarget(Node _target)
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
        List<int> maxUpgradeTier = target.getMaxUpgradeTier();

        int upgradeCode1 = getCanUpgradeForPath(upgradeTier, maxUpgradeTier[0], upgradePath, upgradePath != 1);
        setButton(upgradeButton1, upgradeCostPath1, upgradeCode1, path1, upgradeTier);

        int upgradeCode2 = getCanUpgradeForPath(upgradeTier, maxUpgradeTier[1], upgradePath, upgradePath != 2);
        setButton(upgradeButton2, upgradeCostPath2, upgradeCode2, path2, upgradeTier);
    }

    private int getCanUpgradeForPath(int tier, int maxTier, int currentUpgradePath, bool isOtherPathUpgraded)
    {
        // upgrade path is 0 or the other path was chosen and current path is not 0
        if (maxTier == 0 || (isOtherPathUpgraded && currentUpgradePath != 0))
        {
            return -1;
        }

        // upgrade path is maxed out
        if (tier == maxTier)
        {
            return 0;
        }

        // can upgrade
        return 1;
    }

    private void setButton(Button button, Text text, int code, UpgradePath path, int tier)
    {
        // Button should not show and not be clickable
        if (code == -1)
        {
            text.text = "UNAVAILABLE";
            button.interactable = false;
            return;
        }

        // Upgrade path is finished
        if (code == 0)
        {
            text.text = "DONE";
            button.interactable = false;
            return;
        }

        // Able to upgrade. Apply upgrade stuff
        if (code == 1)
        {
            int pathcost = path.upgrades[tier].cost;
            button.interactable = true;
            text.text = "$" + pathcost;
        }
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    // TODO: hook upgrade button in turret UI to this upgrade function
    public void Upgrade(int path)
    {
        int tier = target.getCurrentUpgradeTier() + 1;
        target.UpgradeTurret(path, tier);
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

}