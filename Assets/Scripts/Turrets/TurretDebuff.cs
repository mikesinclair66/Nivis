using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDebuff : MonoBehaviour
{
    public bool disabled = false;
    private float disableCountdown = 10f;

    public ArrayList activeDebuffs = new ArrayList();
    public GameObject debuffIcon;

    // Activates countdown for the disabled turret and reenables it when the timer hits 0.
    void Update()
    {
        if (disabled == true)
        {
            disableCountdown -= Time.deltaTime;
            if (disableCountdown <= 0)
            {
                Enable();
            }
        }
    }
    
    // Checks the debuffs active
    public ArrayList returnActiveDebuffs()
    {
        return activeDebuffs;
    }
    
    // Disables the turret
    public void Disable()
    {
        disabled = true;
        if (!activeDebuffs.Contains("disabled"))
        {
            activeDebuffs.Add("disabled");
        }
        disableCountdown = 10f;
        debuffIcon.SetActive(true);
    }
    
    // Enables the turret
    public void Enable()
    {
        disabled = false;
        activeDebuffs.Remove("disabled");
        debuffIcon.SetActive(false);
    }
}
