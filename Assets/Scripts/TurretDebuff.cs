using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDebuff : MonoBehaviour
{
    public bool disabled = false;
    private float disableCountdown = 10f;

    public ArrayList activeDebuffs = new ArrayList();
    public GameObject debuffIcon;

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
    
    public ArrayList returnActiveDebuffs()
    {
        return activeDebuffs;
    }
    
    public void Disable()
    {
        disabled = true;
        activeDebuffs.Add("disabled");
        disableCountdown = 10f;
        debuffIcon.SetActive(true);
    }

    public void Enable()
    {
        disabled = false;
        activeDebuffs.Remove("disabled");
        debuffIcon.SetActive(false);
    }
}
