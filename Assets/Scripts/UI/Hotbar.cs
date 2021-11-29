using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// HoverChecker is a custom button component for the Attack1 and ResearchStation
/// buttons which were finnicky within the UI.
/// </summary>
class HoverChecker
{
    public GameObject obj;
    public float sizeX, sizeY, centerX, centerY;
    Color initColor, hoverColor;
    bool hovered = false;

    public HoverChecker(GameObject obj, float sizeX, float sizeY, float centerX, float centerY)
    {
        this.obj = obj;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.centerX = centerX;
        this.centerY = centerY;
        initColor = obj.GetComponent<Image>().color;
        hoverColor = new Color(196, 255, 255);
    }

    public void Update()
    {
        if(!hovered && CanClick())
        {
            obj.GetComponent<Image>().color = hoverColor;
            hovered = true;
        }
        else if(hovered && !CanClick())
        {
            obj.GetComponent<Image>().color = initColor;
            hovered = false;
        }
    }

    public void SetHoverColor(Color hoverColor)
    {
        this.hoverColor = hoverColor;
    }

    private float GetStartX()
    {
        return centerX - sizeX / 2;
    }

    private float GetStartY()
    {
        return centerY + sizeY / 2;
    }

    private float GetEndX()
    {
        return centerX + sizeX / 2;
    }

    private float GetEndY()
    {
        return centerY - sizeY / 2;
    }

    /// <summary>
    /// The onhover condition.
    /// </summary>
    /// <returns>hovered</returns>
    public bool CanClick()
    {
        return Input.mousePosition.x >= GetStartX() && Input.mousePosition.x <= GetEndX()
            && Input.mousePosition.y <= GetStartY() && Input.mousePosition.y >= GetEndY();
    }

    public void ClickButton()
    {
        obj.GetComponent<Button>().onClick.Invoke();
    }

    /// <summary>
    /// Tests if the mouse is in position of the button.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return "mousePos: " + Input.mousePosition.x + ", " + Input.mousePosition.y +
            ", pos: " + GetStartX() + ", " + GetStartY() + ", end: " + GetEndX() + ", " + GetEndY();
    }
}

public class Hotbar : MonoBehaviour
{
    GameObject panel;
    GameObject damageMod;
    GameObject researchBtn;
    GameObject researchStation;
    GameObject attacks, a1;
    GameObject[] attackText, towerText;

    HoverChecker researchChecker, a1Checker;

    void Start()
    {
        panel = GameObject.Find("Canvas/Hotbar");
        damageMod = GameObject.Find("Canvas/Hotbar/Health/HealthMod");
        researchBtn = GameObject.Find("Canvas/Hotbar/ResearchButton");
        researchStation = GameObject.Find("Canvas/ResearchStation");
        attacks = GameObject.Find("Canvas/Hotbar/AttackButtons");
        a1 = GameObject.Find("Canvas/Hotbar/AttackButtons/Button");
        attackText = new GameObject[3];
        towerText = new GameObject[3];
        attackText[0] = GameObject.Find("Canvas/Hotbar/AttackButtons/Button/Text");
        attackText[1] = GameObject.Find("Canvas/Hotbar/AttackButtons/Button2/Text");
        attackText[2] = GameObject.Find("Canvas/Hotbar/AttackButtons/Button3/Text");
        towerText[0] = GameObject.Find("Canvas/Hotbar/TowerButtons/Button/Text");
        towerText[1] = GameObject.Find("Canvas/Hotbar/TowerButtons/Button2/Text");
        towerText[2] = GameObject.Find("Canvas/Hotbar/TowerButtons/Button3/Text");

        Shop shop = GameObject.Find("Canvas/Hotbar/TowerButtons").GetComponent<Shop>();
        attackText[0].GetComponent<Text>().text = "Repair\n\n$" + attacks.GetComponent<Abilities>().reenableTurretCost;
        attackText[1].GetComponent<Text>().text = "Shield\n\n$" + attacks.GetComponent<Abilities>().tempShieldCost;
        attackText[2].GetComponent<Text>().text = "Stun\n\n$" + attacks.GetComponent<Abilities>().stunAreaCost;
        towerText[0].GetComponent<Text>().text = "Normal\nTurret\n$" + shop.standardTurret.cost;
        towerText[1].GetComponent<Text>().text = "Missile\nTurret\n$" + shop.missileLauncher.cost;
        towerText[2].GetComponent<Text>().text = "Pulsor\nTower\n$" + shop.pulsorTurret.cost;

        researchChecker = new HoverChecker(
            researchBtn,
            researchBtn.GetComponent<RectTransform>().rect.width * researchBtn.gameObject.transform.localScale.x,
            (float)Screen.height * 0.09f,
            researchBtn.gameObject.transform.position.x,
            researchBtn.gameObject.transform.position.y
        );
        researchChecker.SetHoverColor(new Color(220, 220, 220));

        a1Checker = new HoverChecker(
            a1,
            (float)attacks.GetComponent<RectTransform>().rect.width,
            (float)Screen.height * 0.09f,
            attacks.gameObject.transform.position.x,
            panel.gameObject.transform.position.y
        );
        a1Checker.sizeX /= 4;
        a1Checker.centerX += a1Checker.sizeX / 2;
        a1Checker.centerY += a1Checker.sizeY / 2;
    }

    /// <summary>
    /// implements the cooldown text effect on the ability buttons.
    /// </summary>
    void Update()
    {
        researchChecker.Update();
        a1Checker.Update();

        if (attacks.GetComponent<Abilities>().getReenableTurretCD() == 0)
        {
            attackText[0].GetComponent<Text>().text = "Repair\n\n$" + attacks.GetComponent<Abilities>().reenableTurretCost;
        }
        else
        {
            attackText[0].GetComponent<Text>().text = "Repair\n\n$" + attacks.GetComponent<Abilities>().reenableTurretCost + "\nCD: " + attacks.GetComponent<Abilities>().getReenableTurretCD();
        }
        
        if (attacks.GetComponent<Abilities>().getTempShieldCD() == 0)
        {
            attackText[1].GetComponent<Text>().text = "Shield\n\n$" + attacks.GetComponent<Abilities>().tempShieldCost;
        }
        else
        {
            attackText[1].GetComponent<Text>().text = "Shield\n\n$" + attacks.GetComponent<Abilities>().tempShieldCost + "\nCD: " + attacks.GetComponent<Abilities>().getTempShieldCD();
        }
        
        if (attacks.GetComponent<Abilities>().getStunAreaCD() == 0)
        {
            attackText[2].GetComponent<Text>().text = "Stun\n\n$" + attacks.GetComponent<Abilities>().stunAreaCost;
        }
        else
        {            
            attackText[2].GetComponent<Text>().text = "Stun\n\n$" + attacks.GetComponent<Abilities>().stunAreaCost + "\nCD: " + attacks.GetComponent<Abilities>().getStunAreaCD();
        }
    }

    public void TakeDamage(int totalHealth)
    {
        damageMod.gameObject.transform.localScale = new Vector3((float)totalHealth / 100.0f, 1, 1);
    }

    /// <summary>
    /// Checks the hotbar's hover-checkers when clicked.
    /// </summary>
    public void RequestClick()
    {
        if (researchChecker.CanClick())
        {
            researchStation.GetComponent<UIAnimator>().Toggle();
        }
        if (a1Checker.CanClick())
        {
            var ped = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(a1Checker.obj, ped, ExecuteEvents.pointerEnterHandler);
            ExecuteEvents.Execute(a1Checker.obj, ped, ExecuteEvents.submitHandler);
        }
    }
}
