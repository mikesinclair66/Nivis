using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    List<GameObject> turrets;
    List<int> turretType, upgradeLvl, upgradePrimary, nodeKey;
    GameObject actionUI, upgradeBtn, upgradeBtn1, upgradeBtn2, upgradeContainer, turretName, researchBtn;
    int towerSelected = -1;

    int primaryBranch = -1;

    void Start()
    {
        turrets = new List<GameObject>();
        turretType = new List<int>();
        upgradeLvl = new List<int>();
        upgradePrimary = new List<int>();
        nodeKey = new List<int>();
        actionUI = GameObject.Find("Canvas/ActionUI");
        upgradeBtn = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeBtn");
        upgradeBtn1 = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeContainer/Btn1");
        upgradeBtn2 = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeContainer/Btn2");
        upgradeContainer = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeContainer");
        turretName = GameObject.Find("Canvas/ActionUI/InnerEl/TurretName");
        researchBtn = GameObject.Find("Canvas/ResearchStation");
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

        for(int i = 0; i < turrets.Count; i++)
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

        actionUI.GetComponent<UIAnimator>().RequestToggle();
    }

    /// <summary>
    /// Tower upgrade.
    /// </summary>
    /// <param name="primaryBranch"></param>
    public void SelectBranch(bool primaryBranch)
    {
        this.primaryBranch = ((primaryBranch) ? 0 : 1);

        upgradeBtn1.SetActive(false);
        upgradeBtn2.SetActive(false);
        Destroy(upgradeBtn1);
        Destroy(upgradeBtn2);
        upgradeBtn.gameObject.transform.SetParent(upgradeContainer.gameObject.transform);
        upgradeBtn.SetActive(true);
        upgradeBtn.gameObject.transform.localScale = upgradeBtn.gameObject.transform.localScale -
            new Vector3(0, 0.45f, 0);
    }
}
