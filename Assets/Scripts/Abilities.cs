using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public BuildManager buildManager;
    public Generator generator;

    public int reenableTurretCost = 100, 
               tempShieldCost = 200, 
               stunAreaCost = 250;

    public float tempShieldDuration = 10;
    public float tempShieldDurationTimeStamp;
    public int tempShieldHP = 30;

    //CD are in seconds
    public float reenableTurretCD = 10,
                 tempShieldCD = 100,
                 stunAreaCD = 100;

    private float reenableTurretTimeStamp,
                  tempShieldTimeStamp,
                  stunAreaTimeStamp;

    private bool reenableTurretOnCD = false,
                 tempShieldOnCD = false,
                 stunAreaOnCD = false;


    void Update()
    {
        if (reenableTurretOnCD == true)
        {
            if (reenableTurretTimeStamp <= Time.time)
            {
                reenableTurretOnCD = false;
            }
        }
        
        if (tempShieldOnCD == true) 
        {
            if (tempShieldTimeStamp <= Time.time)
            {
                tempShieldOnCD = false;
            }

            if (tempShieldDurationTimeStamp <= Time.time)
            {
                generator.shieldHealth = 0;
            }
        }

        if (stunAreaOnCD == true)
        {
            if (stunAreaTimeStamp <= Time.time)
            {
                stunAreaOnCD = false;
            }
        }
        
    }

    public void reenableTurret()
    {
        GameObject turret = buildManager.GetSelectedNode().turret;
        if (turret != null)
        {
            if (reenableTurretOnCD == false)
            {
                if (turret.GetComponent<Turret>().disabled == true)
                {
                    if (buildManager.drill.currentMoney >= reenableTurretCost)
                    {
                        turret.GetComponent<Turret>().Enable();
                        buildManager.drill.currentMoney -= reenableTurretCost;
                        reenableTurretTimeStamp = Time.time + reenableTurretCD;
                        reenableTurretOnCD = true;
                    }
                    else
                    {
                        Debug.Log("you poor");
                    }
                }
                else
                {
                    Debug.Log("Turret not disabled!");
                }
            }
            else
            {
                Debug.Log("Ability on CD!");
            }
        }
    }

    public void tempShield()
    {
        if(tempShieldOnCD == false)
        { 
            if (buildManager.drill.currentMoney >= tempShieldCost)
            {
                generator.shieldHealth = tempShieldHP;
                Debug.Log("HP: " + generator.totalHealth + " Shield: " + generator.shieldHealth);
                buildManager.drill.currentMoney -= tempShieldCost;
                tempShieldDurationTimeStamp = Time.time + tempShieldDuration;
                tempShieldTimeStamp = Time.time + tempShieldCD;
                tempShieldOnCD = true;
            }
            else
            {
                Debug.Log("you poor");
            }
        }
        else
        {
            Debug.Log("Ability on CD!");
        }
    }        

    public void stunArea()
    {

    }

    public int getReenableTurretCD()
    {
        if (reenableTurretOnCD)
        {
            return (int) (reenableTurretTimeStamp - Time.time);
        }
        return 0;
    }
    
    public int getTempShieldCD()
    {
        if (tempShieldOnCD)
        {
            return (int) (tempShieldTimeStamp - Time.time);
        }
        return 0;
    }
    
    public int getStunAreaCD()
    {
        if (stunAreaOnCD)
        {
            return (int) (stunAreaTimeStamp - Time.time);
        }
        return 0;
    }

}
