using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    GameObject panel;
    GameObject towerButton;
    GameObject tbHotbar;
    GameObject tb1, tb2, tb3;
    GameObject tb2Lock, tb3Lock;
    bool tbHotbarToggled = true;

    void Start()
    {
        panel = GameObject.Find("GUI/Panel");
        towerButton = GameObject.Find("GUI/Panel/TowerButton");
        tbHotbar = GameObject.Find("GUI/TBHotbar");
        tb1 = GameObject.Find("GUI/TBHotbar/TB1");
        tb2 = GameObject.Find("GUI/TBHotbar/TB2");
        tb3 = GameObject.Find("GUI/TBHotbar/TB3");
        tb2Lock = GameObject.Find("GUI/TBHotbar/TB2/Lock");
        tb3Lock = GameObject.Find("GUI/TBHotbar/TB3/Lock");

        ToggleTBHotbar();
        panel.SetActive(false);
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
                new Color(1.0f, 1.0f, 1.0f, 0.5f);
        else
            towerButton.GetComponent<Image>().color =
                new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void ClickOther()
    {
        if (tbHotbarToggled)
            ClickTB();
    }
}
