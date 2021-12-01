using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    public BuildManager buildManager;
    public Generator generator;
    public GameObject stunAreaRange;
    public Drill drill;
    public GameObject btn1, btn2, btn3;
    public GameObject alert;

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

    public GameObject shieldIcon;
    public GameObject shieldMod;

    void Start()
    {
        shieldIcon.SetActive(false);
        shieldMod.SetActive(false);
    }

    void Update()
    {
        //updates discernibility of the abilities
        if (drill.currentMoney < reenableTurretCost)
            btn1.GetComponent<Button>().interactable = false;
        else
            btn1.GetComponent<Button>().interactable = true;

        if (drill.currentMoney < tempShieldCost)
            btn2.GetComponent<Button>().interactable = false;
        else
            btn2.GetComponent<Button>().interactable = true;

        if (drill.currentMoney < stunAreaCost)
            btn3.GetComponent<Button>().interactable = false;
        else
            btn3.GetComponent<Button>().interactable = true;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (stunAbilityActive == true)
        {
            if (Physics.Raycast(ray, out hit))
            {
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
                shieldIcon.SetActive(false);
                shieldMod.SetActive(false);
            }

            int shieldModVal = (int)((float)generator.shieldHealth
                / (float)tempShieldHP * 100.0f);
            if (shieldModVal <= 0 || tempShieldDurationTimeStamp <= Time.time)
            {
                generator.shieldHealth = 0;
                generator.mRend.enabled = false;
                shieldIcon.SetActive(false);
                shieldMod.SetActive(false);
            }

            shieldMod.GetComponent<Text>().text = "+" + shieldModVal + "%";
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
                if (turret.GetComponent<Turret>().checkIfDebuffActive("disabled") == true)
                {
                    if (buildManager.drill.currentMoney >= reenableTurretCost)
                    {
                        turret.GetComponent<TurretDebuff>().Enable();
                        buildManager.drill.currentMoney -= reenableTurretCost;
                        reenableTurretTimeStamp = Time.time + reenableTurretCD;
                        reenableTurretOnCD = true;
                        Debug.Log("Turret Re-enabled");
                    }
                    else
                    {
                        alert.GetComponent<AlertHandler>().setAlertText("Cannot Afford Ability", 1.0f);
                    }
                }
                else
                {
                    alert.GetComponent<AlertHandler>().setAlertText("Turret Not Disabled", 1.0f);
                }
            }
            else
            {
                alert.GetComponent<AlertHandler>().setAlertText("Ability On Cool Down", 1.0f);
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
                generator.mRend.enabled = true;
                Debug.Log("HP: " + generator.totalHealth + " Shield: " + generator.shieldHealth);
                buildManager.drill.currentMoney -= tempShieldCost;

                tempShieldDurationTimeStamp = Time.time + tempShieldDuration;
                tempShieldTimeStamp = Time.time + tempShieldCD;
                tempShieldOnCD = true;

                shieldIcon.SetActive(true);
                shieldMod.SetActive(true);
            }
            else
            {
                alert.GetComponent<AlertHandler>().setAlertText("Cannot Afford Ability", 1.0f);
            }
        }
        else
        {
            alert.GetComponent<AlertHandler>().setAlertText("Ability On Cool Down", 1.0f);
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
                alert.GetComponent<AlertHandler>().setAlertText("Cannot Afford Ability", 1.0f);
            }
        }
        else
        {
            alert.GetComponent<AlertHandler>().setAlertText("Ability On Cool Down", 1.0f);
        }
    }

    void Stun(Vector3 hitPos, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPos, radius);
        foreach (Collider c in colliders)
        {
            if (c.GetComponent<Enemy>())
            {
                Debug.Log("stunned!");
                c.GetComponent<Debuff>().activateStun(); // Can add variable to increase length of stun in Enemy.cs
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
