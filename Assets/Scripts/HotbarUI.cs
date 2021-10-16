using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    GameObject towerButton;
    GameObject tbHotbar;
    GameObject tb1, tb2, tb3;
    GameObject tb2Lock, tb3Lock;
    bool tbHotbarToggled = true;

    void Start()
    {
        towerButton = GameObject.Find("Canvas/Hotbar/TowerButton");
        tbHotbar = GameObject.Find("Canvas/Hotbar/TBHotbar");
        tb1 = GameObject.Find("Canvas/Hotbar/TBHotbar/TB1");
        tb2 = GameObject.Find("Canvas/Hotbar/TBHotbar/TB2");
        tb3 = GameObject.Find("Canvas/Hotbar/TBHotbar/TB3");
        tb2Lock = GameObject.Find("Canvas/Hotbar/TBHotbar/TB2/Lock");
        tb3Lock = GameObject.Find("Canvas/Hotbar/TBHotbar/TB3/Lock");

        ToggleTBHotbar();
    }

    void ToggleTBHotbar()
    {
        if (tbHotbarToggled)
        {
            tbHotbar.GetComponent<Image>().color =
                new Color(1.0f, 1.0f, 1.0f, 0);
            tb1.gameObject.SetActive(false);
            tb2.gameObject.SetActive(false);
            tb3.gameObject.SetActive(false);
            tbHotbarToggled = false;
        }
        else
        {
            tbHotbar.GetComponent<Image>().color =
                new Color(1.0f, 1.0f, 1.0f, 1.0f);
            tb1.gameObject.SetActive(true);
            tb2.gameObject.SetActive(true);
            tb3.gameObject.SetActive(true);
            tbHotbarToggled = true;
        }
    }

    public void ClickTB()
    {
        ToggleTBHotbar();
        if (tbHotbarToggled)
            towerButton.GetComponent<Image>().color =
                new Color(1.0f, 1.0f, 1.0f, 0.1f);
        else
            towerButton.GetComponent<Image>().color =
                new Color(1.0f, 1.0f, 1.0f, 0);
    }

    public void ClickOther()
    {
        if (tbHotbarToggled)
            ClickTB();
    }
}
