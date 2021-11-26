using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Drill drill;
    public GameObject btn1, btn2, btn3;
    int awaitButtonHover = -1;
    public OptionDescriptor optionDescriptor;
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint meleeTurret;

    BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    /// <summary>
    /// Updates the discernibility of the buttons.
    /// </summary>
    void Update()
    {
        if (drill.currentMoney < standardTurret.cost)
            btn1.GetComponent<Button>().interactable = false;
        else
            btn1.GetComponent<Button>().interactable = true;

        if (drill.currentMoney < missileLauncher.cost)
            btn2.GetComponent<Button>().interactable = false;
        else
            btn2.GetComponent<Button>().interactable = true;

        if (drill.currentMoney < meleeTurret.cost)
            btn3.GetComponent<Button>().interactable = false;
        else
            btn3.GetComponent<Button>().interactable = true;

        if (awaitButtonHover != -1
            && awaitButtonHover != 100 && Input.mousePosition.y <= 55)
        {
            ButtonHovered(awaitButtonHover);
            awaitButtonHover = 100;
        }
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(standardTurret, 0);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected");
        buildManager.SelectTurretToBuild(missileLauncher, 1);
    }

    public void SelectMeleeTurret()
    {
        Debug.Log("Melee Turret Selected");
        buildManager.SelectTurretToBuild(meleeTurret, 2);
    }

    public void ButtonHovered(int btn)
    {
        if (Input.mousePosition.y <= 55)
        {
            Vector3 nPos = new Vector3(0, btn1.gameObject.transform.position.y + 105, 0);
            string name;

            switch (btn)
            {
                case 0:
                    nPos = nPos + new Vector3(btn1.gameObject.transform.position.x, 0, 0);
                    name = "standard\nturret";
                    break;
                case 1:
                    nPos = nPos + new Vector3(btn2.gameObject.transform.position.x, 0, 0);
                    name = "missile\nlauncher";
                    break;
                case 2:
                default:
                    nPos = nPos + new Vector3(btn3.gameObject.transform.position.x, 0, 0);
                    name = "radiator\ntower";
                    break;
            }

            optionDescriptor.SetPosition(nPos.x, nPos.y, false);
            optionDescriptor.SetText("Purchase the\n" + name);
        }
        else
            awaitButtonHover = btn;
    }

    public void ButtonExited()
    {
        optionDescriptor.Disable();
        awaitButtonHover = -1;
    }
}
