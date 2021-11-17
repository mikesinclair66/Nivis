using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI
{

    // TODO: hook up public variables with game objects in editor
    /*public GameObject ui;

    public Text upgradeCostPath1;
    public Text upgradeCostPath2;
    public Text sellAmount;
    public Button upgradeButton1;
    public Button upgradeButton2;*/

    public int curPathCost;
    public int curSellValue;
    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        //transform.position = target.GetBuildPosition();

        applyUpgradeText();

        //sellAmount.text = "$" + target.turretBlueprint.sellValue;
        curSellValue = target.turretBlueprint.sellValue;

        //ui.SetActive(true);
    }

    public Node GetTarget()
    {
        return target;
    }

    private void applyUpgradeText()
    {
        int upgradeTier = target.getCurrentUpgradeTier();
        int upgradePath = target.getCurrentUpgradePath();
        UpgradePath path1 = target.turretBlueprint.paths[0];
        UpgradePath path2 = target.turretBlueprint.paths[1];
        List<int> maxUpgradeTier = target.getMaxUpgradeTier();

        int upgradeCode1 = getCanUpgradeForPath(upgradeTier, maxUpgradeTier[0], upgradePath, upgradePath != 1);
        setButton(/*upgradeButton1, upgradeCostPath1,*/ upgradeCode1, path1, upgradeTier);

        int upgradeCode2 = getCanUpgradeForPath(upgradeTier, maxUpgradeTier[1], upgradePath, upgradePath != 2);
        setButton(/*upgradeButton2, upgradeCostPath2,*/ upgradeCode2, path2, upgradeTier);
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

    private void setButton(/*Button button, Text text,*/ int code, UpgradePath path, int tier)
    {
        // Button should not show and not be clickable
        if (code == -1)
        {
            //text.text = "UNAVAILABLE";
            //button.interactable = false;
            return;
        }

        // Upgrade path is finished
        if (code == 0)
        {
            //text.text = "DONE";
            //button.interactable = false;
            return;
        }

        // Able to upgrade. Apply upgrade stuff
        if (code == 1)
        {
            int pathcost = path.upgrades[tier].cost;
            //button.interactable = true;
            //text.text = "$" + pathcost;
            curPathCost = pathcost;
        }
    }

    /*public void Hide()
    {
        ui.SetActive(false);
    }*/

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

public class Inventory : MonoBehaviour
{
    List<GameObject> turrets;
    List<int> turretType, upgradeLvl, upgradePrimary, nodeKey;
    GameObject actionUI, actionUIInner, upgradeBtn, upgradeBtn1, upgradeBtn2,
        upgradeContainer, turretName, researchBtn, researchStation, researchStationInner, sellContainer;
    int towerSelected = -1;
    static bool upgradeBtnScaled = false;
    public NodeUI nodeUI = new NodeUI();
    Text sellValueText, upgradeText, upgradeText1, upgradeText2;
    public Drill drill;
    public Shop shop;

    void Awake()
    {
        turrets = new List<GameObject>();
        turretType = new List<int>();
        upgradeLvl = new List<int>();
        upgradePrimary = new List<int>();
        nodeKey = new List<int>();
        actionUI = GameObject.Find("Canvas/ActionUI");
        actionUIInner = GameObject.Find("Canvas/ActionUI/InnerEl");
        upgradeBtn = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeBtn");
        upgradeBtn1 = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeContainer/Btn1");
        upgradeBtn2 = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeContainer/Btn2");
        upgradeContainer = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeContainer");
        turretName = GameObject.Find("Canvas/ActionUI/InnerEl/TurretName");
        researchBtn = GameObject.Find("Canvas/ResearchStation");
        researchStation = GameObject.Find("Canvas/ResearchStation");
        researchStationInner = GameObject.Find("Canvas/ResearchStation/InnerEl");
        sellContainer = GameObject.Find("Canvas/ActionUI/InnerEl/SellContainer");
        sellValueText = GameObject.Find("Canvas/ActionUI/InnerEl/SellContainer/Button/Text")
            .GetComponent<Text>();
        upgradeText = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeBtn/Text")
            .GetComponent<Text>();
        upgradeText1 = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeContainer/Btn1/Text")
            .GetComponent<Text>();
        upgradeText2 = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeContainer/Btn2/Text")
            .GetComponent<Text>();
    }

    void Start()
    {
        upgradeBtn.SetActive(false);
        /*upgradeCostText.text = "-$" + upgradeCost.ToString("0");
        sellValueText.text = "Sell +$" + sellValue.ToString("0");*/
    }

    public void Add(GameObject turret, int type, int nodeKey)
    {
        turrets.Add(turret);
        turretType.Add(type);
        upgradeLvl.Add(0);
        upgradePrimary.Add(-1);
        this.nodeKey.Add(nodeKey);
    }

    /// <summary>
    /// The onclick for turrets.
    /// </summary>
    /// <param name="nodeKey"></param>
    public void SelectTower(int nodeKey)
    {
        int towerNo = -1;

        if (nodeKey == -1)
        {
            actionUI.GetComponent<UIAnimator>().CloseUI();
            researchStation.GetComponent<UIAnimator>().CloseUI();
            BuildManager.instance.DeselectNode();
            return;
        }

        for (int i = 0; i < turrets.Count; i++)
        {
            if (this.nodeKey[i] == nodeKey)
                towerNo = i;
        }

        if (towerNo == -1)
        {
            Debug.LogError("No tower was seleced through Inventory.SelectTower(i)");
            return;
        }

        towerSelected = towerNo;
        Text turretName = this.turretName.GetComponent<Text>();
        switch (turretType[towerSelected])
        {
            case 0:
                turretName.text = "Standard Turret";
                break;
            case 1:
                turretName.text = "Missile Launcher";
                break;
            case 2:
            default:
                turretName.text = "Melee Unit";
                break;
        }

        sellValueText.text = "Sell +$" + nodeUI.curSellValue.ToString("0");

        UpdateUpgradeSystem();
        actionUI.GetComponent<UIAnimator>().RequestToggle();
    }

    bool ResearchUnlocked(int branch, int towerLvl)
    {
        try
        {
            return ResearchStation.researched[turretType[towerSelected], branch, towerLvl];
        }
        catch (NullReferenceException e)
        {
            Debug.Log("You must purchase the achievement to upgrade through the research station.");
            return false;
        }
    }

    /// <summary>
    /// Tower upgrade when selecting branches.
    /// </summary>
    /// <param name="primaryBranch"></param>
    public void SelectBranch(bool primaryBranch)
    {
        int branchNo = ((primaryBranch) ? 0 : 1);
        if (ResearchUnlocked(branchNo, 0))
        {
            int cost;
            switch (turretType[towerSelected])
            {
                case 0:
                    cost = shop.standardTurret.paths[branchNo].upgrades[0].cost;
                    break;
                case 1:
                    cost = shop.missileLauncher.paths[branchNo].upgrades[0].cost;
                    break;
                case 2:
                default:
                    cost = shop.meleeTurret.paths[branchNo].upgrades[0].cost;
                    break;
            }
            if (drill.currentMoney >= cost)
                drill.currentMoney -= cost;
            else
            {
                Debug.Log("Your drill doesn't have enough money!");
                return;
            }
            upgradePrimary[towerSelected] = branchNo;
            upgradeLvl[towerSelected] = 1;
            Debug.Log("Research unlocked. primaryBranch=" + branchNo + ". UpgradeLvl=" + upgradeLvl[towerSelected]);
            //nodeUI.Upgrade(branchNo + 1);
            nodeUI.Upgrade(branchNo + 1);
            actionUI.GetComponent<UIAnimator>().CloseUI();
            researchStation.GetComponent<UIAnimator>().CloseUI();
            //drill.currentMoney -= nodeUI.curPathCost;
            /*
            switch (turretType[towerSelected])
            {
                case 0:
                    drill.currentMoney -= shop.standardTurret.paths[branchNo].upgrades[0].cost;
                    break;
                case 1:
                    drill.currentMoney -= shop.missileLauncher.paths[branchNo].upgrades[0].cost;
                    break;
                case 2:
                default:
                    drill.currentMoney -= shop.meleeTurret.paths[branchNo].upgrades[0].cost;
                    break;
            }
            */
        }
        UpdateUpgradeSystem();
    }

    public void Sell()
    {
        nodeUI.Sell();
        actionUI.GetComponent<UIAnimator>().CloseUI();
        researchStation.GetComponent<UIAnimator>().CloseUI();
        drill.currentMoney += nodeUI.curSellValue;
    }

    public void Upgrade()
    {
        if (upgradeLvl[towerSelected] == 3)
            return;

        if (ResearchUnlocked(upgradePrimary[towerSelected], upgradeLvl[towerSelected]))
        {
            if (upgradeLvl[towerSelected] < 3)
            {
                int cost;
                switch (turretType[towerSelected])
                {
                    case 0:
                        cost = shop.standardTurret
                            .paths[upgradePrimary[towerSelected]].upgrades[upgradeLvl[towerSelected]].cost;
                        break;
                    case 1:
                        cost = shop.missileLauncher
                            .paths[upgradePrimary[towerSelected]].upgrades[upgradeLvl[towerSelected]].cost;
                        break;
                    case 2:
                    default:
                        cost = shop.meleeTurret
                            .paths[upgradePrimary[towerSelected]].upgrades[upgradeLvl[towerSelected]].cost;
                        break;
                }
                if (drill.currentMoney >= cost)
                    drill.currentMoney -= cost;
                else
                {
                    Debug.Log("Your drill doesn't have enough money!");
                    return;
                }
                Debug.Log("Research unlocked. primaryBranch=" + upgradePrimary[towerSelected] + ". UpgradeLvl=" + upgradeLvl[towerSelected]);
                nodeUI.Upgrade(upgradePrimary[towerSelected] + 1);
                actionUI.GetComponent<UIAnimator>().CloseUI();
                researchStation.GetComponent<UIAnimator>().CloseUI();
                //drill.currentMoney -= nodeUI.curPathCost;
                /*
                switch (turretType[towerSelected])
                {
                    case 0:
                        drill.currentMoney -= shop.standardTurret
                            .paths[upgradePrimary[towerSelected]].upgrades[upgradeLvl[towerSelected]].cost;
                        break;
                    case 1:
                        drill.currentMoney -= shop.missileLauncher
                            .paths[upgradePrimary[towerSelected]].upgrades[upgradeLvl[towerSelected]].cost;
                        break;
                    case 2:
                    default:
                        drill.currentMoney -= shop.meleeTurret
                            .paths[upgradePrimary[towerSelected]].upgrades[upgradeLvl[towerSelected]].cost;
                        break;
                }
                */
                upgradeLvl[towerSelected]++;
            }
        }
        UpdateUpgradeSystem();
    }

    /// <summary>
    /// Updates the upgrade ui for the turrets (NOT the drill).
    /// </summary>
    public void UpdateUpgradeSystem()
    {
        if (upgradeLvl[towerSelected] == 3)
        {
            upgradeBtn.GetComponent<Button>().interactable = false;
            upgradeText.text = "DONE";
            return;
        }
        else
            upgradeBtn.GetComponent<Button>().interactable = true;

        if (upgradePrimary[towerSelected] == -1)
        {
            upgradeBtn1.SetActive(true);
            upgradeBtn2.SetActive(true);
            upgradeBtn.SetActive(false);
            //upgradeBtn.gameObject.transform.SetParent(actionUIInner.gameObject.transform);
            //upgradeText1.text =
            switch (turretType[towerSelected])
            {
                case 0:
                    upgradeText1.text = "Sniper\n$" + shop.standardTurret.paths[0].upgrades[0].cost;
                    upgradeText2.text = "Laser\n$" + shop.standardTurret.paths[1].upgrades[0].cost;
                    break;
                case 1:
                    upgradeText1.text = "Nuke\n$" + shop.missileLauncher.paths[0].upgrades[0].cost;
                    upgradeText2.text = "Fire\n$" + shop.missileLauncher.paths[1].upgrades[0].cost;
                    break;
                case 2:
                default:
                    upgradeText1.text = "+Fire\nrate\n$" + shop.meleeTurret.paths[0].upgrades[0].cost;
                    upgradeText2.text = "+Range\n$" + shop.meleeTurret.paths[1].upgrades[0].cost;
                    break;
            }
        }
        else
        {
            upgradeBtn1.SetActive(false);
            upgradeBtn2.SetActive(false);
            upgradeBtn.gameObject.transform.SetParent(upgradeContainer.gameObject.transform);
            upgradeBtn.SetActive(true);
            if (!upgradeBtnScaled)
            {
                upgradeBtn.gameObject.transform.localScale = upgradeBtn.gameObject.transform.localScale -
                    new Vector3(0, 0.45f, 0);
                upgradeBtnScaled = true;
            }

            try
            {
                string descriptor = "";

                switch (turretType[towerSelected])
                {
                    case 0:
                        if (upgradePrimary[towerSelected] == 0)
                        {
                            if (upgradeLvl[towerSelected] == 1)
                                descriptor = "+Damage";
                            if (upgradeLvl[towerSelected] == 2)
                                descriptor = "+Stun";
                        }
                        else if(upgradePrimary[towerSelected] == 1)
                        {
                            if (upgradeLvl[towerSelected] == 1)
                                descriptor = "+Fire\nRate";
                            if (upgradeLvl[towerSelected] == 2)
                                descriptor = "+Freeze %";
                        }
                        upgradeText.text = descriptor + "\n$" + shop.standardTurret.paths[upgradePrimary[towerSelected]]
                            .upgrades[upgradeLvl[towerSelected]].cost;
                        break;
                    case 1:
                        if (upgradePrimary[towerSelected] == 0)
                        {
                            if (upgradeLvl[towerSelected] == 1)
                                descriptor = "+Fire\nRate";
                            if (upgradeLvl[towerSelected] == 2)
                                descriptor = "Radiation";
                        }
                        else if(upgradePrimary[towerSelected] == 1)
                        {
                            if (upgradeLvl[towerSelected] == 1)
                                descriptor = "+Damage";
                            if (upgradeLvl[towerSelected] == 2)
                                descriptor = "+Fire\nSpread";
                        }
                        upgradeText.text = descriptor + "\n$" + shop.missileLauncher.paths[upgradePrimary[towerSelected]]
                            .upgrades[upgradeLvl[towerSelected]].cost;
                        break;
                    case 2:
                    default:
                        if (upgradePrimary[towerSelected] == 0)
                        {
                            if (upgradeLvl[towerSelected] == 1)
                                descriptor = "2x Kill\nCounts";
                            if (upgradeLvl[towerSelected] == 2)
                                descriptor = "+Instant\nKill %";
                        }
                        else if(upgradePrimary[towerSelected] == 1)
                        {
                            if (upgradeLvl[towerSelected] == 1)
                                descriptor = "Collect\nEnemy $";
                            if (upgradeLvl[towerSelected] == 2)
                                descriptor = "+Damage";
                        }
                        upgradeText.text = descriptor + "\n$" + shop.meleeTurret.paths[upgradePrimary[towerSelected]]
                            .upgrades[upgradeLvl[towerSelected]].cost;
                        break;
                }
            } catch(ArgumentOutOfRangeException e)
            {
                upgradeBtn.GetComponent<Button>().interactable = false;
                upgradeText.text = "DONE";
            }
        }
    }
}