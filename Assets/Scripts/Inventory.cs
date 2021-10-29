using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Hotbar
{
    string hotbarName;
    GameObject hotbar;
    GameObject b1, b2, b3;
    GameObject b2Lock, b3Lock, b2Text, b3Text;
    bool b2Locked = true, b3Locked = true;

    public Hotbar(string hotbarName)
    {
        this.hotbarName = hotbarName;
        hotbar = GameObject.Find("Canvas/Hotbar/" + hotbarName);
        b1 = GameObject.Find("Canvas/Hotbar/" + hotbarName + "/B1");
        b2 = GameObject.Find("Canvas/Hotbar/" + hotbarName + "/B2");
        b3 = GameObject.Find("Canvas/Hotbar/" + hotbarName + "/B3");
        b2Lock = GameObject.Find("Canvas/Hotbar/" + hotbarName + "/B2/Lock");
        b3Lock = GameObject.Find("Canvas/Hotbar/" + hotbarName + "/B3/Lock");
        b2Text = GameObject.Find("Canvas/Hotbar/" + hotbarName + "/B2/Text");
        b3Text = GameObject.Find("Canvas/Hotbar/" + hotbarName + "/B3/Text");

        b2Text.SetActive(false);
        b3Text.SetActive(false);
    }

    public void SetVisible(bool visible)
    {
        hotbar.SetActive(visible);
    }

    public void SetLocked(int lockNo, bool locked)
    {
        switch (lockNo)
        {
            case 0:
                b2Lock.SetActive(locked);
                b2Text.SetActive(!locked);
                b2Locked = locked;
                break;
            case 1:
                b3Lock.SetActive(locked);
                b3Text.SetActive(!locked);
                b3Locked = locked;
                break;
        }
    }
}

public class Inventory : MonoBehaviour
{
    bool hotbarToggled = true;

    GameObject panel;
    GameObject towerBtn;
    Color unselectedColor;
    Hotbar tbHotbar, unitHotbar;

    void Start()
    {
        panel = GameObject.Find("Canvas/Hotbar");
        towerBtn = GameObject.Find("Canvas/Hotbar/TowerButton");
        unselectedColor = towerBtn.GetComponent<Image>().color;

        tbHotbar = new Hotbar("TBHotbar");
        unitHotbar = new Hotbar("UnitHotbar");

        ToggleHotbar();
        SetLocked(false, 0, 0);
    }

    public void ToggleHotbar()
    {
        hotbarToggled = !hotbarToggled;
        tbHotbar.SetVisible(hotbarToggled);
        unitHotbar.SetVisible(hotbarToggled);

        Image tb = towerBtn.GetComponent<Image>();
        if (hotbarToggled)
            tb.color = Color.white;
        else
            tb.color = unselectedColor;
    }

    /// <summary>
    /// locked - bool
    /// btnNo - the button to toggle the lock on, starting from the second button onwards
    /// hotbarNo - the hotbar to toggle the lock on
    /// </summary>
    /// <param name="tbNo"></param>
    /// <param name="btnNo"></param>
    public void SetLocked(bool locked, int hotbarNo, int lockNo)
    {
        if (hotbarNo == 0)
            tbHotbar.SetLocked(lockNo, locked);
        else if (hotbarNo == 1)
            unitHotbar.SetLocked(lockNo, locked);
    }
}
