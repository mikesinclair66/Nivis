using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    GameObject icons, burn, radiation, stun, slow, freeze;
    GameObject healthMod;

    bool visible = true;

    void Awake()
    {
        icons = transform.GetChild(0).gameObject;
        burn = icons.gameObject.transform.GetChild(0).gameObject;
        radiation = icons.gameObject.transform.GetChild(1).gameObject;
        stun = icons.gameObject.transform.GetChild(2).gameObject;
        slow = icons.gameObject.transform.GetChild(3).gameObject;
        freeze = icons.gameObject.transform.GetChild(4).gameObject;

        healthMod = transform.GetChild(1).GetChild(0).gameObject;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(90 * (float)Math.PI / 180, 180 * (float)Math.PI / 180, 0));
    }

    public void SetHealth(float curHealth, float defHealth)
    {
        healthMod.transform.localScale = new Vector3(curHealth / defHealth, 1, 1);
    }

    public void SetHealth(float scale)
    {
        healthMod.transform.localScale = new Vector3(scale, 1, 1);
    }

    public void SetVisible(bool visible)
    {
        this.visible = visible;
        gameObject.SetActive(visible);
    }

    public bool IsVisible()
    {
        return visible;
    }

    /// <summary>
    /// Sets activeness of icon in the order:
    /// -burn
    /// -radiation
    /// -stun
    /// -slow
    /// -freeze
    /// </summary>
    public void SetDebuffActive(string debuff, bool active)
    {
        switch (debuff)
        {
            case "burning":
                burn.SetActive(active);
                break;
            case "radiated":
                radiation.SetActive(active);
                break;
            case "stunned":
                stun.SetActive(active);
                break;
            case "slowed":
                slow.SetActive(active);
                break;
            case "frozen":
            default:
                freeze.SetActive(active);
                break;
        }
    }

    public void DeactivateAllDebuffs()
    {
        burn.SetActive(false);
        radiation.SetActive(false);
        stun.SetActive(false);
        slow.SetActive(false);
        freeze.SetActive(false);
    }
}
