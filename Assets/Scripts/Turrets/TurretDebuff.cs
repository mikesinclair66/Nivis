using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDebuff : MonoBehaviour
{
    public bool disabled = false;
    private float disableCountdown = 10f;

    public ArrayList activeDebuffs = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        if (!activeDebuffs.Contains("disabled"))
        {
            activeDebuffs.Add("disabled");
        }
        disableCountdown = 10f;
    }

    public void Enable()
    {
        disabled = false;
        activeDebuffs.Remove("disabled");
    }
}
