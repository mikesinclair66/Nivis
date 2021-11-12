using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    List<GameObject> turrets;
    List<int> turretType, upgradeLvl, upgradePrimary, nodeKey;
    GameObject actionUI, actionUIInner, upgradeBtn, upgradeBtn1, upgradeBtn2,
        upgradeContainer, turretName, researchBtn, researchStation, researchStationInner;
    int towerSelected = -1;
    static bool upgradeBtnScaled = false;
    public GameObject nodeUI;

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
    }

    void Start()
    {
        upgradeBtn.SetActive(false);
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
            upgradePrimary[towerSelected] = branchNo;
            upgradeLvl[towerSelected] = 1;
            UpdateUpgradeSystem();
            //nodeUI.Upgrade(branchNo + 1);
            nodeUI.GetComponent<NodeUI>().Upgrade(branchNo + 1);
            actionUI.GetComponent<UIAnimator>().CloseUI();
            researchStation.GetComponent<UIAnimator>().CloseUI();
        }
    }

    public void Upgrade()
    {
        if (ResearchUnlocked(upgradePrimary[towerSelected], upgradeLvl[towerSelected]))
        {
            if (upgradeLvl[towerSelected] < 3)
                upgradeLvl[towerSelected]++;
            //nodeUI.Upgrade(upgradePrimary[towerSelected] + 1);
            nodeUI.GetComponent<NodeUI>().Upgrade(upgradePrimary[towerSelected] + 1);
            actionUI.GetComponent<UIAnimator>().CloseUI();
            researchStation.GetComponent<UIAnimator>().CloseUI();
        }
    }

    public void UpdateUpgradeSystem()
    {
        if (upgradePrimary[towerSelected] == -1)
        {
            upgradeBtn1.SetActive(true);
            upgradeBtn2.SetActive(true);
            upgradeBtn.SetActive(false);
            upgradeBtn.gameObject.transform.SetParent(actionUIInner.gameObject.transform);
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
        }
    }
}