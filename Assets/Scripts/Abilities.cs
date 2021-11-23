using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public BuildManager buildManager;
    public Generator generator;
    public GameObject stunAreaRange;

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

    private bool stunAbilityActive = false;

    public bool reenableTurretIsRequested = false;

    void Update()
    {
        if (stunAbilityActive == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SpawnAoEStun(stunAreaRange);
                stunAbilityActive = false;
                Debug.Log("Stun Active: " + stunAbilityActive);
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
        stunAbilityActive = true;
        Debug.Log("Stun Active: " + stunAbilityActive);
    }

    void SpawnAoEStun(GameObject objToSpawn)
    {
        Vector3 mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPos = new Vector3(hit.point.x, hit.point.y + objToSpawn.transform.position.y, hit.point.z);
            GameObject objClone = Instantiate(objToSpawn, hitPos, Quaternion.identity);
            Debug.Log("Spawned Stun");
            Stun(hitPos);
            Destroy(objClone); //destroys object right after stunning
        }
    }

    void Stun(Vector3 hitPos)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPos, 8); // Debating for Variable of Range or some other way? Radius is an INT
        foreach (Collider c in colliders)
        {
            if (c.GetComponent<Enemy>())
            {
                Debug.Log("STUNNED!");
                c.GetComponent<Enemy>().activateStun(); // Can add variable to increase length of stun in Enemy.cs
            }
        }
        //use colliders with the object spawned in SpawnAoEStun
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
