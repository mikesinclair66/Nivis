using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public BuildManager buildManager;
    public Generator generator;
    public GameObject stunAreaRange;

    public int reenableTurretCost = 50,
               tempShieldCost = 75,
               stunAreaCost = 125;

    public float tempShieldDuration = 10;
    public float tempShieldDurationTimeStamp;
    public int tempShieldHP = 30;

    //CD are in seconds
    public float reenableTurretCD = 10,
                 tempShieldCD = 30,
                 stunAreaCD = 30;

    private float reenableTurretTimeStamp,
                  tempShieldTimeStamp,
                  stunAreaTimeStamp;

    private bool reenableTurretOnCD = false,
                 tempShieldOnCD = false,
                 stunAreaOnCD = false;

    private bool stunAbilityActive = false;

    public bool reenableTurretIsRequested = false;

    private GameObject obj;
    private Ray ray;
    private RaycastHit hit;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (stunAbilityActive == true)
        {
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                obj.transform.position = hit.point;

                if (Input.GetMouseButtonDown(0))
                {
                    float colliderRadius = obj.GetComponent<SphereCollider>().radius;
                    Stun(obj.transform.position, colliderRadius);
                    Destroy(obj);
                    stunAbilityActive = false;
                    Debug.Log("Stun Active: " + stunAbilityActive);
                }
            } 
        }

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

    public void requestReenableTurret()
    {
        reenableTurretIsRequested = true;
    }

    public void reenableTurret(GameObject turret)
    {
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
                        Debug.Log("Turret Re-enabled");
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
        if (tempShieldOnCD == false)
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
        obj = Instantiate(stunAreaRange, hit.point, Quaternion.identity);
        if (stunAreaOnCD == false)
        {
            if (buildManager.drill.currentMoney >= stunAreaCost)
            {
                stunAbilityActive = true;
                Debug.Log("Stun Active: " + stunAbilityActive);
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

    void Stun(Vector3 hitPos, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPos, radius);
        foreach (Collider c in colliders)
        {
            if (c.GetComponent<Enemy>())
            {
                Debug.Log("STUNNED!");
                c.GetComponent<Enemy>().activateStun(); // Can add variable to increase length of stun in Enemy.cs
            }
        }

        buildManager.drill.currentMoney -= stunAreaCost;

        stunAreaTimeStamp = Time.time + stunAreaCD;
        stunAreaOnCD = true;
    }

    public int getReenableTurretCD()
    {
        if (reenableTurretOnCD)
        {
            return (int)(reenableTurretTimeStamp - Time.time);
        }
        return 0;
    }

    public int getTempShieldCD()
    {
        if (tempShieldOnCD)
        {
            return (int)(tempShieldTimeStamp - Time.time);
        }
        return 0;
    }

    public int getStunAreaCD()
    {
        if (stunAreaOnCD)
        {
            return (int)(stunAreaTimeStamp - Time.time);
        }
        return 0;
    }

}
