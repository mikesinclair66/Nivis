using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public bool CanClick()
    {
        return Input.mousePosition.x >= GetStartX() && Input.mousePosition.x <= GetEndX()
            && Input.mousePosition.y <= GetStartY() && Input.mousePosition.y >= GetEndY();
    }

    public void ClickButton()
    {
        obj.GetComponent<Button>().onClick.Invoke();
    }

    public override string ToString()
    {
        return "mousePos: " + Input.mousePosition.x + ", " + Input.mousePosition.y +
            ", pos: " + GetStartX() + ", " + GetStartY() + ", end: " + GetEndX() + ", " + GetEndY();
    }
}

public class Hotbar : MonoBehaviour
{
    GameObject canvas;
    GameObject panel;
    GameObject damageMod;
    GameObject researchBtn;
    GameObject researchStation;
    GameObject attacks, a1;
    GameObject[] attackText, towerText;//button cost
    public int a1Text, a2Text, a3Text;
    GameObject u1, u2, uMain, uContainer;
    GameObject eventSystem;

    HoverChecker researchChecker, a1Checker;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
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
        u1 = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeContainer/Btn1");
        u2 = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeContainer/Btn2");
        uMain = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeBtn");
        uContainer = GameObject.Find("Canvas/ActionUI/InnerEl/UpgradeContainer");
        eventSystem = GameObject.Find("EventSystem");

        Shop shop = GameObject.Find("Canvas/Hotbar/TowerButtons").GetComponent<Shop>();
        attackText[0].GetComponent<Text>().text = "1\n\n$" + a1Text;
        attackText[1].GetComponent<Text>().text = "2\n\n$" + a2Text;
        attackText[2].GetComponent<Text>().text = "3\n\n$" + a3Text;
        towerText[0].GetComponent<Text>().text = "1\n\n$" + shop.standardTurret.cost;
        towerText[1].GetComponent<Text>().text = "2\n\n$" + shop.missileLauncher.cost;
        towerText[2].GetComponent<Text>().text = "3\n\n$" + shop.meleeTurret.cost;

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

    void Update()
    {
        researchChecker.Update();
        a1Checker.Update();
        //Debug.Log(upgrade1.ToString());
    }

    public void TakeDamage(int totalHealth)
    {
        damageMod.gameObject.transform.localScale = new Vector3((float)totalHealth / 100.0f, 1, 1);
    }

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
