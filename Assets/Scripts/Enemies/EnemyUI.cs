using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    private GameObject icons, burn, radiation, stun, slow, freeze;
    private GameObject healthMod;

    public Enemy enemy;
    public Debuff debuff;

    private ArrayList activeDebuffs = new ArrayList();
    private ArrayList allDebuffs = new ArrayList();

    private float defaultHealth, totalHealth;

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

    void Start()
    {
        activeDebuffs = debuff.returnActiveDebuffs();
        allDebuffs = debuff.returnAllDebuffs();

        DeactivateAllDebuffs();

        defaultHealth = enemy.GetDefaultHealth();
        totalHealth = enemy.totalHealth;
    }

    /// <summary>
    /// Updates the health bar and checks active debuffs.
    /// </summary>
    void Update()
    {
        activeDebuffs = debuff.returnActiveDebuffs();
        checkDebuffs();
        
        if (enemy.totalHealth != totalHealth)
        {
            totalHealth = enemy.totalHealth;
            if (!IsVisible())
                SetVisible(true);
            SetHealth(totalHealth, defaultHealth);
        }
        transform.rotation = Quaternion.LookRotation(new Vector3(90 * (float)Math.PI / 180, 180 * (float)Math.PI / 180, 0));
    }

    public void SetHealth(float curHealth, float defHealth)
    {
        healthMod.transform.localScale = new Vector3(curHealth / defHealth, 1, 1);
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
    /// Checks which debuffs are active and updates the GUI.
    /// </summary>
    public void checkDebuffs()
    {
        foreach (string debuffType in allDebuffs)
        {
            SetDebuffActive(debuffType, activeDebuffs.Contains(debuffType));
        }
    }

    /// <summary>
    /// Sets activeness of icon in the order:
    /// -burn
    /// -radiation
    /// -stun
    /// -slow
    /// -freeze
    /// </summary>
    public void SetDebuffActive(string debuffType, bool isActive)
    {
        // switch (debuffType)
        // {
        //     case debuff.debuffBurning:
        //         burn.SetActive(isActive);
        //         break;
        //     case debuff.debuffRadiated:
        //         radiation.SetActive(isActive);
        //         break;
        //     case debuff.debuffStunned:
        //         stun.SetActive(isActive);
        //         break;
        //     case debuff.debuffSlowed:
        //         slow.SetActive(isActive);
        //         break;
        //     case debuff.debuffFrozen:
        //         freeze.SetActive(isActive);
        //         break;
        //     default:
        //         break;
        // }

        if (debuffType == debuff.debuffBurning)
        {
            burn.SetActive(isActive);
        }
        if (debuffType == debuff.debuffRadiated)
        {
            radiation.SetActive(isActive);
        }
        if (debuffType == debuff.debuffStunned)
        {
            stun.SetActive(isActive);
        }
        if (debuffType == debuff.debuffSlowed)
        {
            slow.SetActive(isActive);
        }
        if (debuffType == debuff.debuffFrozen)
        {
            freeze.SetActive(isActive);
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
